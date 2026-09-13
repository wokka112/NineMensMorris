using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private static int MAIN_MENU_SCENE = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(DelayedReturnToMainMenu());
    }

    // Update is called once per frame
    void Update()
    {
        // If user hits LMB, RMB or ESC, return to main menu.
        if (Input.GetMouseButtonDown(0)  || Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
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
