using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.pitch = s.pitch;

            s.source.playOnAwake = s.playOnAwake;
        }
    }

    private void OnEnable()
    {
        PlayerController.Shoot += Play;
        PlayerController.Punch += Play;
    }

    private void OnDisable()
    {
        PlayerController.Shoot -= Play;
        PlayerController.Punch -= Play;
    }

    private void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("There's an error");
            return;
        }
        s.source.Play();
    }
}
