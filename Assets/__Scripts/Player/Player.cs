using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpSpeed = 3f;

    private Rigidbody2D _rigidbody;
    private GroundDetec _groundDetec;
    private Animator _animator;

    public event Action Death;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetec = GetComponentInChildren<GroundDetec>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector2 velcocity = _rigidbody.velocity;

        velcocity.x = 0;

        if (Input.GetKey(KeyCode.A))
        {
            velcocity.x = -_speed ;
        }
        if(Input.GetKey(KeyCode.D)) 
        {
            velcocity.x = _speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            if (_groundDetec.IsGrounded)
            {
                velcocity.y = _jumpSpeed;
            }
        }

        _rigidbody.velocity = velcocity;

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
        yield return new WaitForSeconds(2.5f);
        Death?.Invoke();
        Destroy(this.gameObject);
    }

    private void Picked()
    {
        Vector2 velocity = new Vector2(0, 5);
        _rigidbody.velocity = velocity;
    }

}
