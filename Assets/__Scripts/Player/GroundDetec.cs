using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GroundDetec : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _mask;

    private bool _isGrounded;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
    }

    private void FixedUpdate()
    {
        Collider2D col = Physics2D.OverlapCircle(transform.position, _radius, _mask);
        _isGrounded = col != null;
    }
}
