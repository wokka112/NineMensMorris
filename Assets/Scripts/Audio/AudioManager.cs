using System;
using Unity.Mathematics;
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

    [Tooltip("The minimum decibel value for the volume")]
    [SerializeField]
    private float minDb = -80.0f;
    [Tooltip("The maximum decibel value for the volume")]
    [SerializeField]
    private float maxDb = 0.0f;

    [Tooltip("The minimum decimal value for the volume sliders")]
    [SerializeField]
    private float minDecimal = 0.0f;
    [Tooltip("The minimum decimal value for the volume sliders")]
    [SerializeField]
    private float maxDecimal = 1.0f;
    [Tooltip("Additionally you can scale the volume along a nonlinear curve to give more precision in the middle to top range.")]
    [Range(0.1f, 1f)]
    public float linearity = 1.0f;


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

    public void SetEffectsVolumeFromDecimal(float decimalVolume)
    {
        SetVolumeFromDecimal("effectsVolume", decimalVolume);
    }

    public float GetDecimalEffectsVolume()
    {
        return GetDecimalVolume("effectsVolume");
    }

    public void SetMusicVolumeFromDecimal(float decimalVolume)
    {
        SetVolumeFromDecimal("musicVolume", decimalVolume);
    }

    public float GetDecimalMusicVolume()
    {
        return GetDecimalVolume("musicVolume");
    }

    private float GetDecimalVolume(string name)
    {
        audioMixer.GetFloat(name, out float decibelVolume);
        if (decibelVolume <= minDb)
        {
            return minDecimal;
        }

        if (decibelVolume >= maxDb)
        {
            return maxDecimal;
        }

        var normalized = math.saturate(math.remap(minDb, maxDb, minDecimal, maxDecimal, decibelVolume));
        var nonlinear = math.pow(normalized, 1f / linearity);
        var exponential = math.pow(10f, nonlinear);
        var remapped = math.remap(1, 10, minDecimal, maxDecimal, exponential);

        return remapped;
    }

    private void SetVolumeFromDecimal(string volumeName, float decimalVolume)
    {
        if (decimalVolume <= minDecimal)
        {
            audioMixer.SetFloat(volumeName, minDb);
        } else if (decimalVolume >= maxDecimal)
        {
            audioMixer.SetFloat(volumeName, maxDb);
        } else
        {
            var remapped = math.remap(minDecimal, maxDecimal, 1, 10, decimalVolume);
            var logarithmic = math.saturate(math.log10(remapped));
            var nonlinear = math.pow(logarithmic, linearity);
            var decibels = math.remap(minDecimal, maxDecimal, minDb, maxDb, nonlinear);

            audioMixer.SetFloat(volumeName, decibels);
        }
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
