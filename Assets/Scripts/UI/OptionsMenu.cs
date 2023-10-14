using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    //TODO add logic for volume
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject previousMenu;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(Back);
    }

    private void Back()
    {
        gameObject.SetActive(false);
        previousMenu.SetActive(true);
    }
}
