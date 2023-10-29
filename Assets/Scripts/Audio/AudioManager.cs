using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] effectSounds;
    public Sound[] music;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private AudioMixerGroup effectsMixerGroup;
    [SerializeField]
    private AudioMixerGroup musicMixerGroup;


    void Awake()
    {
        foreach(Sound sound in effectSounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = effectsMixerGroup;
            sound.SetSource(source);
        }

        foreach(Sound track in music)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = musicMixerGroup;
            track.SetSource(source);
        }

        if (music[0] != null)
        {
            music[0].SetLoop(true);
            music[0].Play();
        }
    }

    public void SetEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }

    public float GetEffectsVolume()
    {
        audioMixer.GetFloat("effectsVolume", out float volume);
        return volume;
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public float GetMusicVolume()
    {
        audioMixer.GetFloat("musicVolume", out float volume);
        return volume;
    }

    public void PlaySound(string name)
    {
        Sound s = FindSound(name);
        s.Play();
    }

    public void PlayLoopSound(string name)
    {
        Sound s = FindSound(name);
        s.SetLoop(true);
        s.Play();
    }

    public void PlayMusic(string name)
    {
        Sound track = FindMusic(name);
        track.Play();
    }

    public void PlayLoopMusic(string name)
    {
        Sound track = FindMusic(name);
        track.SetLoop(true);
        track.Play();
    }

    private Sound FindSound(string name)
    {
        return Array.Find(effectSounds, sound => sound.GetName() == name);
    }

    private Sound FindMusic(string name)
    {
        return Array.Find(music, track => track.GetName() == name);
    }
}
