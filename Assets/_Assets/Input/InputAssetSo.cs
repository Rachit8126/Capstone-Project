using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Scriptable Objects/Input/Input Asset", fileName = "New Input Asset")]
public class InputAssetSo : ScriptableObject, PlayerInputActions.IPlayerActions
{
    public event Action OnPlayerPrimaryInteract;
    public event Action<Vector3> OnPlayerMove;
    public event Action OnPlayerJump;
    public event Action OnPlayerOpenInventory;
    public event Action<bool> OnPlayerSetUiState;

    public Vector2 LookVector { get; private set; }
    
    private PlayerInputActions _playerInput;

    private void OnEnable()
    {
        if (_playerInput == null)
        {
            _playerInput = new PlayerInputActions();
            _playerInput.Player.SetCallbacks(this);
        }

        _playerInput.Enable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 inputVector = context.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);
        OnPlayerMove?.Invoke(moveDirection);
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookVector = context.ReadValue<Vector2>();
    }

    public void OnPrimaryInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerPrimaryInteract?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPlayerJump?.Invoke();
        }
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        OnPlayerOpenInventory?.Invoke();
        OnPlayerSetUiState?.Invoke(true);
    }

    public void UiClosed()
    {
        OnPlayerSetUiState?.Invoke(false);
    }

    public void SetCursorState(bool state)
    {
        Cursor.visible = state;
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
