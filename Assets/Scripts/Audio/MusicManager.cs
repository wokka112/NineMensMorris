using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private string menuMusicName;
    [SerializeField]
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting music manager");
        audioManager.PlayLoop(menuMusicName);
    }
}
