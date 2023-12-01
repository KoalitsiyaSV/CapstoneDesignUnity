using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundEffectSource;
    public AudioSource musicSource;

    private Dictionary<string, AudioClip> soundEffectLibrary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        soundEffectLibrary = new Dictionary<string, AudioClip>();
        AudioClip[] soundEffects = Resources.LoadAll<AudioClip>("SoundEffects");
        foreach (AudioClip clip in soundEffects)
        {
            soundEffectLibrary.Add(clip.name, clip);
        }
    }

    public void PlaySoundEffect(string soundName, float volume = 1.0f)
    {
        if (soundEffectLibrary.ContainsKey(soundName))
        {
            soundEffectSource.PlayOneShot(soundEffectLibrary[soundName], volume);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void PlayMusic(AudioClip music, float volume = 1.0f)
    {
        musicSource.clip = music;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}