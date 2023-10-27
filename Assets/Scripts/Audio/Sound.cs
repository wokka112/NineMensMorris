using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    private const float pitchMinVal = .1f;
    private const float pitchMaxVal = 3f;

    [SerializeField]
    private AudioClip clip;
    [SerializeField]
    private string name;
    [SerializeField]
    [Range(0, 1f)]
    private float volume;
    [SerializeField]
    [Range(pitchMinVal, pitchMaxVal)]
    private float pitch;

    private AudioSource source;

    public string GetName()
    {
        return name;
    }

    public void SetSource(AudioSource source)
    {
        if (this.source != null)
            RemoveSource();

        this.source = source;
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
    }

    public void RemoveSource()
    {
        source.Stop();
        source.clip = null;
        source.volume = 0f;
        source.pitch = 0f;
        source = null;
    }

    public AudioSource GetSource()
    {
        return source;
    }

    public void SetVolume(float volume)
    {
        this.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void SetPitch(float pitch)
    {
        this.pitch = pitch;
    }

    public float GetPitch()
    {
        return pitch;
    }

    public void SetLoop(bool shouldLoop)
    {
        source.loop = shouldLoop;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }
}
