using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputActionsProvider
{
    private static InputActions inputActions;

    public static System.Action OnClickStarted;
    public static System.Action OnClickCanceled;

    public static void Initialize()
    {
        inputActions = new InputActions();
        inputActions.Player.Enable();

        inputActions.Player.Click.started += Click_started;
        inputActions.Player.Click.canceled += Click_canceled;
    }

    private static void Click_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnClickStarted?.Invoke();
    }

    private static void Click_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnClickCanceled?.Invoke();
    }

    public static Vector2 GetMousePosition()
    {
        return UnityEngine.InputSystem.Mouse.current.position.ReadValue();
    }
}
