using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    InputAction clickAction;
    InputAction rightClickAction;
    InputAction escapeAction;

    private static int MAIN_MENU_SCENE = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedReturnToMainMenu());
        clickAction = InputSystem.actions.FindAction("Click");
        rightClickAction = InputSystem.actions.FindAction("RightClick");
        escapeAction = InputSystem.actions.FindAction("Escape");
    }

    // Update is called once per frame
    void Update()
    {
        // If user hits LMB, RMB or ESC, return to main menu.
        if (clickAction.IsPressed() || rightClickAction.IsPressed() || escapeAction.IsPressed())
        {
            ReturnToMainMenu();
        }  

    }

    private IEnumerator DelayedReturnToMainMenu()
    {
        yield return new WaitForSeconds(10f);
        ReturnToMainMenu();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU_SCENE);
    }
}
