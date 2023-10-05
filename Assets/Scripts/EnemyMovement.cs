using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 1f;
    Rigidbody2D _myRb2D;


    void Start()
    {
        _myRb2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _myRb2D.velocity = new Vector2(_moveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            _moveSpeed = -_moveSpeed;
            FlipEnemyFacing();
        }
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_myRb2D.velocity.x)), 1f);
    }
}
