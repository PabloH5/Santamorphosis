using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsController : MonoBehaviour
{
    private PlayerInputs _playerInputs;
    private MovementController _movementController;
    private MetamorphosisController _metamorphosisController;

    private void Awake() {
        _playerInputs = new();

        _movementController = GetComponent<MovementController>();
    }

    #region Events Subscription
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
    #endregion

    private void OnMove(InputAction.CallbackContext context)
    {
        if(!_metamorphosisController.isTransforming)
        {
            _movementController._moveInput = context.ReadValue<Vector2>();
        }
    }

    private void CancelMove(InputAction.CallbackContext context)
    {
        _movementController._moveInput = Vector2.zero;
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed && _movementController.dashTimer >= _movementController.dashCoolDown && !_metamorphosisController.isTransforming)
        {
            // Perform dash
            _movementController.Dash();
        }
    }
}
