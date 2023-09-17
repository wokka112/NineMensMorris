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

    public void DisplayPromptText(float displayTimeInMs)
    {
        DisplayText(promptText);
        StartCoroutine(WaitAndHide(promptText, displayTimeInMs));
    }

    public void HidePromptText()
    {
        HideText(promptText);
    }

    public void DisplayTurnText()
    {
        DisplayText(turnText);
    }

    public void DisplayTurnText(float displayTimeInMs)
    {
        DisplayText(turnText);
        StartCoroutine(WaitAndHide(turnText, displayTimeInMs));
    }

    public void HideTurnText()
    {
        HideText(turnText);
    }

    public void DisplayImportantText()
    {
        DisplayText(importantText);
    }

    public void DisplayImportantText(float displayTimeInMs)
    {
        DisplayText(importantText);
        StartCoroutine(WaitAndHide(importantText, displayTimeInMs));
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

    private IEnumerator WaitAndHide(TextMeshProUGUI text, float displayTimeInMs)
    {
        yield return new WaitForSeconds(displayTimeInMs);
        HideText(text);
    }
}
