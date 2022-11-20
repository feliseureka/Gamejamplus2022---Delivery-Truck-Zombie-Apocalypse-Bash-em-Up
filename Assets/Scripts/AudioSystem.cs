using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioSystem : MonoBehaviour
{
    public static AudioSystem Instance;
    public AudioSource musicSource, SFXsource, EngineLoopSFX, zombieSFX;

    public AudioClip[] MusicArray;
    public AudioClip[] DriftArray;
    public AudioClip[] ZombieArray;
    public AudioClip[] EngineLoop;
    int sfxplay = -1;

    void Start(){
        if(Instance != null && Instance != this){
            Destroy(gameObject);
        }else{
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        if(PlayerPrefs.HasKey("MusicVol")) ChangeMusicVolume(PlayerPrefs.GetFloat("MusicVol"));
        if(PlayerPrefs.HasKey("SFXVol")) ChangeSFXVolume(PlayerPrefs.GetFloat("SFXVol"));
    }

    public void PlayMusic(int i){
        AudioClip s = MusicArray[i];
        musicSource.clip = s;
        musicSource.Play();
    }

    public void PlayEngine(int i){
        if(!EngineLoopSFX.isPlaying){
            AudioClip s = EngineLoop[i];
            EngineLoopSFX.clip = s;
            EngineLoopSFX.Play();
        }
    }

    public void PlayZombie(){
        int i = UnityEngine.Random.Range(0, ZombieArray.Length);
        if(!zombieSFX.isPlaying){
            AudioClip s = ZombieArray[i];
            zombieSFX.clip = s;
            zombieSFX.Play();
            PlayZombie();
        }
    }
    public void PlayDrift(int i){
        if(!SFXsource.isPlaying){
            AudioClip s = DriftArray[i];
            SFXsource.clip = s;
            SFXsource.Play();
        }
    }

    public void StopDrift(){
        SFXsource.Stop();
    }

    public void ChangeMusicVolume(float value){
        musicSource.volume = value;
    }

    public void ChangeSFXVolume(float value){
        SFXsource.volume = value;
        EngineLoopSFX.volume = value;
        zombieSFX.volume = value;
    }

    public void tMuteMusic(){
        musicSource.mute = !musicSource.mute;
    }

    public void tMuteSFX(){
        SFXsource.mute = !SFXsource.mute;
        EngineLoopSFX.mute = SFXsource.mute;
        zombieSFX.mute = SFXsource.mute;
    }

    public void StopMusic(){
        musicSource.Stop();
    }

    public int retSFXPlaying(){
        return sfxplay;
    }

    public void StopSFX(){
        SFXsource.Stop();
        EngineLoopSFX.Stop();
        zombieSFX.Stop();
    }

    public void StopEngine(){
        EngineLoopSFX.Stop();
    }
}
