using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EventsController : MonoBehaviour
{
    private PlayerInputs _playerInputs;
    private MovementController _movementController;
    private MetamorphosisController _metamorphosisController;
    private EatController _eatController;

    private void Awake()
    {
        _movementController = GetComponent<MovementController>();
        _metamorphosisController = GetComponent<MetamorphosisController>();
        _eatController = GetComponent<EatController>();

        _playerInputs = new();
    }
    private void Update() {
        _movementController.canMove = !_metamorphosisController.isTransforming;
    }

    #region Events Subscription
    private void OnEnable()
    {
        _playerInputs.Player.Enable();
        _playerInputs.Transformation.Enable();
        _playerInputs.Interact.Enable();

        _playerInputs.Player.Movement.performed += OnMove;
        _playerInputs.Player.Movement.canceled += CancelMove;

        _playerInputs.Player.Dash.performed += OnDash;

        _playerInputs.Interact.Eat.performed += OnEat;
        _playerInputs.Interact.Eat.canceled += OnEatCancel;


        // Get the transformation map again to subscribe actions
        InputActionMap transformationMap = _playerInputs.Transformation.Get();

        foreach (var action in transformationMap.actions)
        {
            action.performed += OnTransform;
        }

    }

    private void OnDisable()
    {
        _playerInputs.Player.Disable();
        _playerInputs.Transformation.Disable();
        _playerInputs.Interact.Enable();

        _playerInputs.Player.Movement.performed -= OnMove;
        _playerInputs.Player.Movement.canceled -= CancelMove;

        _playerInputs.Player.Dash.performed -= OnDash;

        _playerInputs.Interact.Eat.performed -= OnEat;
        _playerInputs.Interact.Eat.canceled -= OnEatCancel;

        // Get the transformation map again to unsubscribe actions
        InputActionMap transformationMap = _playerInputs.Transformation.Get();

        foreach (var action in transformationMap.actions)
        {
            action.performed -= OnTransform;
        }
    }
    #endregion

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!_metamorphosisController.isTransforming)
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

    private void OnEat(InputAction.CallbackContext context)
    {
        _eatController.isEating = context.performed;
    }
    private void OnEatCancel(InputAction.CallbackContext context)
    {
        _eatController.isEating = false;
    }

    private void OnTransform(InputAction.CallbackContext context)
    {
        switch (context.action.name)
        {
            case "TransformBack":
                _metamorphosisController.TransformController(0);
                break;

            case "Item1":
                _metamorphosisController.TransformController(1);
                break;

            case "Item2":
                _metamorphosisController.TransformController(2);
                break;

            case "Item3":
                _metamorphosisController.TransformController(3);
                break;

            default:
                _metamorphosisController.TransformController(0);
                break;
        }
    }
}
