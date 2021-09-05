using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource SoundEffects;
    public AudioSource Music;
    public AudioSource Ambience;
    public AudioSource orb;

    public static SoundManager Instance = null;
    private float masterVol = 0.8f;
    private float musicVol = 1f;
    private float sfxVol = 1.0f;

    public float musVol = 0.5f;
    public float ambienceVol = 0.2f;

    public AudioClip orbDiscover;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    
    // // Start is called before the first frame update
    // void Start()
    // {
    //     masterVol = 0.8f;
    //     musicVol = 1;
    //     sfxVol = 1;
    // }

    public void SetMasterVolume(float vol)
    {
        masterVol = vol;
        Music.volume = musVol* masterVol*musicVol;
        SoundEffects.volume = 1 * masterVol * sfxVol;
         Ambience.volume = ambienceVol * masterVol * sfxVol;
        Debug.Log("2222" + Music.volume);
    }

    public void SetMusicVolume(float vol)
    {
        Debug.Log("Setting Music vol to..." + vol);
        musicVol = vol;
        Music.volume = musVol* masterVol*musicVol;
        Debug.Log("Music1" + Music.volume);
    }

    public float getMaster()
    {
        return masterVol;
    }

    public float getSFX()
    {
        return sfxVol;
    }

    public float getMusic()
    {
        return musicVol;
    }

    public float getSFXVolume()
    {
        return masterVol * sfxVol;
    }

    public void SetSFXVolume(float vol)
    {
        sfxVol = vol;
        SoundEffects.volume = 1 * masterVol * sfxVol;
        Ambience.volume = ambienceVol * masterVol * sfxVol;
    }

    public void PlaySound(AudioClip sound)
    {
        //Debug.Log("Sound played");
        SoundEffects.volume = 1 * masterVol * sfxVol;
        SoundEffects.clip = sound;
        SoundEffects.Play();
    }

    public void playOrbDiscover()
    {
        orb.volume = 1 * masterVol * sfxVol * 0.6f;
        orb.clip = orbDiscover;
        orb.Play();
    }

    public void PlaySound(AudioClip sound, float vol)
    {
        //Debug.Log("Sound played");
        SoundEffects.volume = vol* masterVol* sfxVol;
        SoundEffects.clip = sound;
        SoundEffects.Play();
    }

    public void PlayAmbience(AudioClip sound, float vol)
    {
        //Debug.Log("Sound played");
        Ambience.volume = vol * masterVol * sfxVol;
        Ambience.clip = sound;
        Ambience.Play();
    }

    public void PlayAmbience(AudioClip sound)
    {
        //Debug.Log("Sound played");
        Ambience.volume = ambienceVol * masterVol * sfxVol;
        Ambience.clip = sound;
        Ambience.Play();
    }

    public void PauseNonMusic()
    {
        Debug.Log("WHY");
        SoundEffects.Pause();
        Ambience.Pause();
    }

    public void PlayNonMusic()
    {
        SoundEffects.Play();
        Ambience.Play();
    }

    public void StopAllSound()
    {
        SoundEffects.Pause();
        Music.Pause();
        Ambience.Pause();
    }

    public void PlayMusic(AudioClip song)
    {
        Debug.Log("PLYAER MUSIC");
        Music.volume = musVol* masterVol*musicVol;
        Music.clip = song;
        Music.Play();
    }

    public void PlayMusic(AudioClip song, float vol)
    {
        Debug.Log("Playing Music" + vol * masterVol * musicVol);
        Music.clip = song;
        Music.volume = vol* masterVol*musicVol * musVol;
        Music.Play();
    }

    public void StopMusic()
    {
        Music.Pause();
    }
}
