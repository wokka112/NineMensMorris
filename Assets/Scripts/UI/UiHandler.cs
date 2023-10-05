using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    //TODO better way to deal with this than display items and queue system?
    private LinkedList<DisplayItem> promptDisplayItems;
    private DisplayItem currentPrompt;
    private float currentPromptDisplayTime;

    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private TextMeshProUGUI endMenuWinnerText;
    [SerializeField]
    private GameObject pauseMenu;

    public void Awake()
    {
        promptDisplayItems = new LinkedList<DisplayItem>();
        currentPromptDisplayTime = 0f;
    }

    public void FixedUpdate()
    {
        currentPromptDisplayTime += Time.deltaTime;
        float currentPromptMinDisplayTime = currentPrompt != null ? currentPrompt.GetMinDisplayTime() : 0f;
        if (promptDisplayItems.Count > 0 && (currentPromptDisplayTime >= currentPromptMinDisplayTime))
        {
            currentPrompt = promptDisplayItems.First.Value;
            SetPromptText(currentPrompt.GetDisplayText());
            DisplayPromptText();
            promptDisplayItems.RemoveFirst();

            currentPromptDisplayTime = 0;
        } else if (currentPrompt != null && currentPrompt.ShouldHideAfter() && (currentPromptDisplayTime >= currentPromptMinDisplayTime))
        {
            currentPrompt = null;
            HidePromptText();
            currentPromptDisplayTime = 0;
        }
    }

    public void AddPromptText(string text)
    {
        promptDisplayItems.AddLast(new DisplayItem(text, 0, false));
    }

    public void AddPromptText(string text, float minimumDisplayTime, bool shouldHideAfter)
    {
        promptDisplayItems.AddLast(new DisplayItem(text, minimumDisplayTime, shouldHideAfter));
    }

    public void ClearPromptItems()
    {
        promptDisplayItems.Clear();
    }
    public void DisplayEndMenu()
    {
        endMenu.SetActive(true);
    }

    public void HideEndMenu()
    {
        endMenu.SetActive(false);
    }

    public void DisplayPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public bool IsPauseMenuDisplayed()
    {
        return pauseMenu.activeSelf == true;
    }

    public bool IsEndMenuDisplayed()
    {
        return endMenu.activeSelf == true;
    }

    public void HidePromptText()
    {
        promptText.gameObject.SetActive(false);
    }

    public void SetWinner(Colour? winner)
    {
        if (winner == null)
        {
            Debug.LogError("Tried setting the winner in the UI without there being a winner!");
        } else
        {
            endMenuWinnerText.SetText(winner.ToString() + " won!");
        }
    }

    private void DisplayPromptText()
    {
        promptText.gameObject.SetActive(true);
    }

    private void SetPromptText(string text)
    {
        promptText.text = text;
    }
}
