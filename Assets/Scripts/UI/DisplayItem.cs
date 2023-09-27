public class DisplayItem
{
    private readonly string displayText;
    private readonly float minDisplayTime;
    private readonly bool shouldHideAfter;

    public DisplayItem(string displayText, float displayTime, bool shouldHideAfter)
    {
        this.displayText = displayText;
        this.minDisplayTime = displayTime;
        this.shouldHideAfter = shouldHideAfter;
    }

    public string GetDisplayText()
    {
        return displayText;
    }

    public float GetMinDisplayTime()
    {
        return minDisplayTime;
    }

    public bool ShouldHideAfter()
    {
        return shouldHideAfter;
    }
}
