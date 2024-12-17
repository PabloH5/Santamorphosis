
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class MovementController : MonoBehaviour
{
    private PlayerInputs _playerInputs;

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    [Header("Movement parameters")]
    [SerializeField] private float moveSpeed;

    private void Awake() {
        _playerInputs = new();
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    #region InputsEvents

    //Subscribe to the events
    private void OnEnable() 
    {
        _playerInputs.Player.Enable();

        _playerInputs.Player.Movement.performed += OnMove;
        _playerInputs.Player.Movement.canceled += CancelMove;

    }

    //Unsubscribe to the events
    private void OnDisable() 
    {
        _playerInputs.Player.Disable();

        _playerInputs.Player.Movement.performed -= OnMove;
        _playerInputs.Player.Movement.canceled -= CancelMove;

    }

    //Method to get the input value 
    private void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    //Method for stop all movement switching to zero
    private void CancelMove(InputAction.CallbackContext context)
    {
        _moveInput = Vector2.zero;
    }
    #endregion

    private void HandleMovement()
    {
        //apply to the RigidBody2D velocity in the direction of the input at velocity of moveSpeed
        _rb.velocity =new Vector2(_moveInput.x * moveSpeed, _moveInput.y * moveSpeed);
    }
}
