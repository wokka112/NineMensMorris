using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }

    public void Play(string name)
    {
        Sound s = FindSound(name);
        s.Play();
    }

    public void PlayLoop(string name)
    {
        Debug.Log("Play loop: " + name);
        Sound s = FindSound(name);
        Debug.Log("Found sound: " + name);
        s.SetLoop(true);
        s.Play();
    }

    private Sound FindSound(string name)
    {
        return Array.Find(sounds, sound => sound.GetName() == name);
    }
}
