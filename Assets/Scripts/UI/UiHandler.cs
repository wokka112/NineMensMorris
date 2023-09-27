using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    private LinkedList<DisplayItem> promptDisplayItems;
    private DisplayItem currentPrompt;
    private float currentPromptDisplayTime;

    private LinkedList<DisplayItem> importantDisplayItems;
    private DisplayItem currentImportant;
    private float currentImportantDisplayTime;

    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private TextMeshProUGUI importantText;

    public void Awake()
    {
        promptDisplayItems = new LinkedList<DisplayItem>();
        currentPromptDisplayTime = 0f;

        importantDisplayItems = new LinkedList<DisplayItem>();
        currentImportantDisplayTime = 0f;
    }

    public void FixedUpdate()
    {
        currentPromptDisplayTime += Time.deltaTime;
        float currentPromptMinDisplayTime = currentPrompt != null ? currentPrompt.GetMinDisplayTime() : 0f;
        if (promptDisplayItems.Count > 0 && (currentPromptDisplayTime >= currentPromptMinDisplayTime))
        {
            currentPrompt = promptDisplayItems.First.Value;
            SetText(promptText, currentPrompt.GetDisplayText());
            DisplayText(promptText);
            promptDisplayItems.RemoveFirst();

            currentPromptDisplayTime = 0;
        } else if (currentPrompt != null && currentPrompt.ShouldHideAfter() && (currentPromptDisplayTime >= currentPromptMinDisplayTime))
        {
            currentPrompt = null;
            HidePromptText();
            currentPromptDisplayTime = 0;
        }

        currentImportantDisplayTime += Time.deltaTime;
        float currentImportantMinDisplayTime = currentImportant != null ? currentImportant.GetMinDisplayTime() : 0f;
        if (importantDisplayItems.Count > 0 && (currentImportantDisplayTime >= currentImportantMinDisplayTime))
        {
            currentImportant = importantDisplayItems.First.Value;
            SetText(importantText, currentImportant.GetDisplayText());
            DisplayText(importantText);
            importantDisplayItems.RemoveFirst();

            currentImportantDisplayTime = 0;
        } else if (currentImportant != null && currentImportant.ShouldHideAfter() && currentImportantDisplayTime >= currentImportantMinDisplayTime)
        {
            currentImportant = null;
            HideImportantText();
            currentImportantDisplayTime = 0;
        }
    }

    public void SetPromptText(string text)
    {
        promptDisplayItems.AddLast(new DisplayItem(text, 0, false));
    }

    public void SetPromptText(string text, float minimumDisplayTime, bool shouldHideAfter)
    {
        promptDisplayItems.AddLast(new DisplayItem(text, minimumDisplayTime, shouldHideAfter));
    }

    public void SetImportantText(string text)
    {
        importantDisplayItems.AddLast(new DisplayItem(text, 0, false));
    }

    public void SetImportantText(string text, float minimumDisplayTime, bool shouldHideAfter)
    {
        importantDisplayItems.AddLast(new DisplayItem(text, minimumDisplayTime, shouldHideAfter));
    }

    public void ClearPromptItems()
    {
        promptDisplayItems.Clear();
    }

    public void HidePromptText()
    {
        HideText(promptText);
    }

    public void ClearImportantItems()
    {
        importantDisplayItems.Clear();
    }

    public void HideImportantText()
    {
        HideText(importantText);
    }

    private void DisplayText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    private void SetText(TextMeshProUGUI textElement, string text)
    {
        textElement.text = text;
    }

    private void HideText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }
}
