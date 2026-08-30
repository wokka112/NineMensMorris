using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider effectsVolumeSlider;
    [SerializeField]
    private Slider musicVolumeSlider;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private GameObject previousMenu;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        backButton.onClick.AddListener(Back);
        backButton.onClick.AddListener(PlayClick);

        effectsVolumeSlider.SetValueWithoutNotify(audioManager.GetDecimalEffectsVolume());
        musicVolumeSlider.SetValueWithoutNotify(audioManager.GetDecimalMusicVolume());

        effectsVolumeSlider.onValueChanged.AddListener(audioManager.SetEffectsVolumeFromDecimal);
        musicVolumeSlider.onValueChanged.AddListener(audioManager.SetMusicVolumeFromDecimal);
    }

    private void Back()
    {
        gameObject.SetActive(false);
        previousMenu.SetActive(true);
    }

    private void PlayClick()
    {
        audioManager.PlaySound("click");
    }
}
