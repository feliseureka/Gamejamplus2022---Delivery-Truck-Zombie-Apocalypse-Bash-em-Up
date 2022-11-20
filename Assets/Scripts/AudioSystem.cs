using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance;
    public AudioSource musicSource, SFXsource;

    public AudioClip[] MusicArray;
    public AudioClip[] SFXArray;
    int sfxplay = -1;

    void Start(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if(PlayerPrefs.HasKey("MusicVol")) musicSource.volume = PlayerPrefs.GetFloat("MusicVol");
        if(PlayerPrefs.HasKey("SFXVol")) SFXsource.volume = PlayerPrefs.GetFloat("SFXVol");
    }

    public void PlayMusic(int i){
        AudioClip s = MusicArray[i];
        musicSource.clip = s;
        musicSource.Play();
    }

    public void PlaySFX(int i){
        if(!SFXsource.isPlaying){
            sfxplay = i;
            AudioClip s = SFXArray[i];
            SFXsource.clip = s;
            SFXsource.Play();
        }
    }

    public void ChangeMusicVolume(float value){
        musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value){
        SFXsource.volume = value;
    }

    public void tMuteMusic(){
        musicSource.mute = !musicSource.mute;
    }

    public void tMuteSFX(){
        SFXsource.mute = !SFXsource.mute;
    }

    public void StopMusic(){
        musicSource.Stop();
    }

    public int retSFXPlaying(){
        return sfxplay;
    }

    public void StopSFX(){
        SFXsource.Stop();
    }
}
