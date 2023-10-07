using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //public static SoundManager instance;
    public enum Bgm { Village, Dungeon }
    public enum PlayerSound { attack }
    public AudioClip[] bgmAudioClips;

    //SoundManager soundManager; 
    AudioSource audios;


    private void Awake()
    {
        /*#region ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(instance.gameObject);
        #endregion*/


        audios = GetComponent<AudioSource>();
        
    }

        //soundManager = GetComponent<SoundManager>(); 
        //soundManager.PlayBgm(SoundManager.Bgm.bgm1);

    public void PlayBgm(Bgm bgm)
    {
        audios.clip = bgmAudioClips[(int)bgm];
        audios.Play();
    }
}
