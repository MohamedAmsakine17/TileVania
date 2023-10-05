using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] float _moveSpeed = 10f;
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _climbSpeed = 5f;

    [Header("Shoot")]
    [SerializeField] GameObject _bullet;
    [SerializeField] Transform _gun;

    [Header("Death")]
    [SerializeField]
    Vector2 _deathKick = new Vector2(10f, 10f);


    Vector2 _moveInput;
    Rigidbody2D _myRb2D;
    Animator _myAnimator;
    CapsuleCollider2D _myBodyCollider2D;
    BoxCollider2D _myFeetCollider2D;

    float _currentGravity;
    bool _isAlive = true;


    void Start()
    {
        _myRb2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        _myFeetCollider2D = GetComponent<BoxCollider2D>();
        _currentGravity = _myRb2D.gravityScale;
    }

    void Update()
    {
        if (!_isAlive) { return; }

        Run();
        FlipSprtie();
        climbLadder();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!_isAlive) { return; }
        // get the vector 2 form the input value when you click in move buttons
        _moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!_isAlive) { return; }

        if (!_myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            _myRb2D.velocity += new Vector2(0f, _jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!_isAlive) { return; }

        if (value.isPressed)
        {
            _myAnimator.SetTrigger("Shoot");
            Instantiate(_bullet, _gun.position, transform.rotation);
        }
    }

    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(_moveInput.x * _moveSpeed, _myRb2D.velocity.y);
        _myRb2D.velocity = PlayerVelocity;

        bool playerHasHorizontalMove = Mathf.Abs(_myRb2D.velocity.x) > Mathf.Epsilon;
        _myAnimator.SetBool("IsRunning", playerHasHorizontalMove);
    }

    void FlipSprtie()
    {
        bool playerHasHorizontalMove = Mathf.Abs(_myRb2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalMove)
            transform.localScale = new Vector2(Mathf.Sign(_myRb2D.velocity.x), 1f);
    }

    void climbLadder()
    {
        if (!_myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            _myRb2D.gravityScale = _currentGravity;
            _myAnimator.SetBool("IsDumbling", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(_myRb2D.velocity.x, _moveInput.y * _climbSpeed);
        _myRb2D.velocity = climbVelocity;
        _myRb2D.gravityScale = 0;

        bool playerHasVerticalMove = Mathf.Abs(_myRb2D.velocity.y) > Mathf.Epsilon;
        _myAnimator.SetBool("IsDumbling", playerHasVerticalMove);

    }

    void Die()
    {
        if (_myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            _isAlive = false;
            _myAnimator.SetTrigger("Die");
            _myRb2D.velocity = _deathKick;
            FindObjectOfType<GameSession>().PlayerSession();
        }
    }
}
