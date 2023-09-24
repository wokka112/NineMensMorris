using System.Collections;
using TMPro;
using UnityEngine;

public class UiHandler : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private TextMeshProUGUI turnText;
    [SerializeField]
    private TextMeshProUGUI importantText;

    public void SetPromptText(string text)
    {
        promptText.text = text;
    }

    public void SetTurnText(string text)
    {
        turnText.text = text;
    }

    public void SetImportantText(string text)
    {
        importantText.text = text;
    }

    public void DisplayPromptText()
    {
        DisplayText(promptText);
    }

    public void DisplayPromptText(float displayTimeInSeconds)
    {
        DisplayText(promptText);
        StartCoroutine(WaitAndHide(promptText, displayTimeInSeconds));
    }

    public void HidePromptText()
    {
        HideText(promptText);
    }

    public void DisplayTurnText()
    {
        DisplayText(turnText);
    }

    public void DisplayTurnText(float displayTimeInSeconds)
    {
        DisplayText(turnText);
        StartCoroutine(WaitAndHide(turnText, displayTimeInSeconds));
    }

    public void HideTurnText()
    {
        HideText(turnText);
    }

    public void DisplayImportantText()
    {
        DisplayText(importantText);
    }

    public void DisplayImportantText(float displayTimeInSeconds)
    {
        DisplayText(importantText);
        StartCoroutine(WaitAndHide(importantText, displayTimeInSeconds));
    }

    public void HideImportantText()
    {
        HideText(importantText);
    }

    private void DisplayText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    private void HideText(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }

    private IEnumerator WaitAndHide(TextMeshProUGUI text, float displayTimeInSeconds)
    {
        yield return new WaitForSeconds(displayTimeInSeconds);
        HideText(text);
    }
}
