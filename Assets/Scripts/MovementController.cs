using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    private PlayerInputs _playerInputs;
    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Dash parameters")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCoolDown = 2f;
    private float dashTimer;
    private bool isDashing;
    private float dashEndTime;

    private void Awake() {
        _playerInputs = new();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        dashTimer += Time.deltaTime;

        // If we are currently dashing, check if the dash duration has ended
        if (isDashing && Time.time >= dashEndTime)
        {
            // Dash is over
            isDashing = false;
        }
    }

    private void FixedUpdate()
    {
        // If currently dashing, do not apply normal movement
        if (!isDashing)
        {
            HandleMovement();
        }
    }

    #region InputsEvents

    private void OnEnable() 
    {
        _playerInputs.Player.Enable();

        _playerInputs.Player.Movement.performed += OnMove;
        _playerInputs.Player.Movement.canceled += CancelMove;

        _playerInputs.Player.Dash.performed += OnDash;
    }

    private void OnDisable() 
    {
        _playerInputs.Player.Disable();

        _playerInputs.Player.Movement.performed -= OnMove;
        _playerInputs.Player.Movement.canceled -= CancelMove;

        _playerInputs.Player.Dash.performed -= OnDash;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && dashTimer >= dashCoolDown)
        {
            // Perform dash
            Dash();
        }
    }

    #endregion

    private void HandleMovement()
    {
        //apply to the RigidBody2D velocity in the direction of the input at velocity of moveSpeed
        _rb.velocity =_moveInput * moveSpeed;
    }
    private void Dash()
    {
        // Set dashing state
        isDashing = true;
        dashEndTime = Time.time + dashDuration;
        dashTimer = 0f;

        // Apply a high velocity directly (instead of AddForce)
        // This ensures immediate high-speed movement without waiting for physics
        _rb.velocity = _moveInput.normalized * dashSpeed;

        Debug.Log("DASH");
    }
}
