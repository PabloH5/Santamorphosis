using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private Rigidbody2D _rb;
    public Vector2 _moveInput;
    public AudioSource audioSource;

    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash parameters")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    public float dashCoolDown = 2f;
    public float dashTimer;
    public bool isDashing;
    public bool canMove = true;
    private float dashEndTime;
    [SerializeField] private Animator _animatorSanta;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        dashTimer += Time.deltaTime;

        // If we are currently dashing, check if the dash duration has ended
        if (isDashing && Time.time >= dashEndTime)
        {
            // Dash is over
            isDashing = false;
        }

        // If currently dashing, do not apply normal movement
        if (!isDashing)
        {
            HandleMovement();
        }
        //Setting the horizontal and vertical movement from "moveInput" vector for animations
        _animatorSanta.SetFloat("Vertical", _moveInput.x);
        _animatorSanta.SetFloat("Horizontal", _moveInput.y);
    }

    private void HandleMovement()
    {
        if(canMove)
        {
            //apply to the RigidBody2D velocity in the direction of the input at velocity of moveSpeed
            _rb.velocity = _moveInput * moveSpeed;
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }
    public void Dash()
    {
        // Set dashing state
        isDashing = true;
        dashEndTime = Time.time + dashDuration; //start dash timer
        dashTimer = 0f;

        // Apply a high velocity in movement direction
        _rb.velocity = _moveInput.normalized * dashSpeed;
        audioSource.Play();
    }
}
