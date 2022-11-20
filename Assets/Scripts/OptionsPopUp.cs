using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsPopUp : MonoBehaviour
{
    public GameObject OptionsUI;
    public Slider musicSlider;
    public Slider SFXSlider;
    public TMP_Text musicText;
    public TMP_Text sfxText;

    void Start(){
        if(!PlayerPrefs.HasKey("MusicVol")) PlayerPrefs.SetFloat("MusicVol", 100f);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        musicText.text = Mathf.Floor(100 * musicSlider.value) + "";
        if(!PlayerPrefs.HasKey("MusicVol")) PlayerPrefs.SetFloat("SFXVol", 100f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVol");
        sfxText.text = Mathf.Floor(100 * SFXSlider.value) + "";
    }

    public void changeMusicSlider(){
        AudioSystem.Instance.ChangeMusicVolume(musicSlider.value);
        musicText.text = Mathf.Floor(100 * musicSlider.value) + "";
        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }

    public void changeSFXSlider(){
        AudioSystem.Instance.ChangeSFXVolume(SFXSlider.value);
        sfxText.text = Mathf.Floor(100 * SFXSlider.value) + "";
        PlayerPrefs.SetFloat("SFXVol", SFXSlider.value);
    }

    public void muteMusic(){
        AudioSystem.Instance.tMuteMusic();
    }

    public void muteSFX(){
        AudioSystem.Instance.tMuteSFX();
    }

    public void openOptionUI(){
        OptionsUI.SetActive(true);
    }
}