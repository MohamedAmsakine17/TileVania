using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [SerializeField] float _bulletSpeed;
    Rigidbody2D _rb2D;
    PlayerMovement _player;
    float _xSpeed;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * _bulletSpeed;
        transform.localScale = new Vector2(Mathf.Sign(_xSpeed), 1f);
    }

    void Update()
    {
        _rb2D.velocity = new Vector2(_xSpeed, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);

    }
}
