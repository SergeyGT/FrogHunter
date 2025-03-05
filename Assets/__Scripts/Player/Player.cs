using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpSpeed = 3f;
    [SerializeField] private float _gravityScale = 2f;
    [SerializeField] private float _coyoteTime = 0.1f;
    [SerializeField] private float _landingSmoothTime = 0.1f;
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private ParticleSystem _doubleJump;
    [SerializeField] private float _distanceToFan;

    private Rigidbody2D _rigidbody;
    private GroundDetec _groundDetec;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private float _coyoteTimer;
    private int _jumpCount = 1;

    public static event Action Death;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetec = GetComponentInChildren<GroundDetec>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Vector2 velocity = _rigidbody.velocity;
        velocity.x = 0;

        if (Input.GetKey(KeyCode.A))
        {
            velocity.x = -_speed;
            _spriteRenderer.flipX = true;
            _animator.SetBool("Run", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velocity.x = _speed;
            _spriteRenderer.flipX = false;
            _animator.SetBool("Run", true);
        }
        else
        {
            _animator.SetBool("Run", false);
        }

        
        if (_groundDetec.IsGrounded)
        {
            _coyoteTimer = _coyoteTime;
            _jumpCount = 0;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }

        
        if (Input.GetKeyDown(KeyCode.W) && (_coyoteTimer > 0 || _jumpCount < _maxJumps - 1))
        {
            if (_jumpCount == 2) 
            { 
                velocity.y = _jumpSpeed * 0.8f;
                ParticleSystem effect = Instantiate(_doubleJump, transform.position, Quaternion.identity);
                effect.Play();
                Destroy(effect.gameObject, 1f);
            }
            else velocity.y = _jumpSpeed;
            _jumpCount++; 
            _coyoteTimer = 0; 
        }

       
        if (!_groundDetec.IsGrounded)
        {
            velocity.y += Physics2D.gravity.y * (_gravityScale - 1) * Time.deltaTime;
        }

        
        if (_groundDetec.IsGrounded && velocity.y < 0)
        {
            velocity.y = Mathf.Lerp(velocity.y, 0, _landingSmoothTime);
        }

        _rigidbody.velocity = velocity;
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _distanceToFan, LayerMask.GetMask("Fan"));
        Debug.DrawRay(transform.position, Vector2.down * _distanceToFan, Color.red);
  
        if (hit.collider != null && hit.collider.gameObject.CompareTag("Fan"))
        {
                Vector2 velocity = _rigidbody.velocity;
            velocity.y = Mathf.Lerp(velocity.y, 5f, 0.6f);
                _rigidbody.velocity = velocity;
        }
    }

    private void OnEnable()
    {
        Pick.Picked += TakeDamage;
    }

    private void OnDisable()
    {
        Pick.Picked -= TakeDamage;
    }

    private void TakeDamage(bool obj)
    {
        StartCoroutine(WaitDeath(obj));
    }

    private IEnumerator WaitDeath(bool obj)
    {
        _animator.SetBool("Hit", obj);
        Picked();
        yield return new WaitForSeconds(0.5f);
        Death?.Invoke();
        Destroy(this.gameObject);
    }

    private void Picked()
    {
        Vector2 velocity = new Vector2(0, 5);
        _rigidbody.velocity = velocity;
    }
}
