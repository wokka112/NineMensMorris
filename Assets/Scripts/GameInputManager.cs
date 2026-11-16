using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInputManager : MonoBehaviour
{
    InputAction clickAction;
    InputAction rightClickAction;
    InputAction escapeAction;
    InputAction pointAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clickAction = InputSystem.actions.FindAction("Click");
        rightClickAction = InputSystem.actions.FindAction("RightClick");
        escapeAction = InputSystem.actions.FindAction("Escape");
        pointAction = InputSystem.actions.FindAction("Point");
    }

    public bool IsClickClicked()
    {
        return clickAction.IsPressed();
    }

    public bool IsRightClickClicked() { return rightClickAction.IsPressed(); }

    public bool IsEscapePressed() { return escapeAction.IsPressed(); }

    public Vector2 GetMousePoint()
    {
        return pointAction.ReadValue<Vector2>();
    }
}
