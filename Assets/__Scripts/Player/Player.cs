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

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundDetec = GetComponentInChildren<GroundDetec>();
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
}
