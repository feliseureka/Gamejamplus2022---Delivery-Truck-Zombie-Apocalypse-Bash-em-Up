using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    public SkillSO skill;
    public TMP_Text text;
    public PlayerStats PStat;

    void OnEnable(){
        text.text = skill.skillName;
    }

    public void onCardClick(){
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1;
        if(!skill.isLoop){
            if(skill.level < skill.maxLevel-1){
                skill.level++;
            }else if(skill.level == skill.maxLevel - 1){
                skill.level++;
                PopUpUpgradeSystem.SkillArray.Remove(skill);
            }
            addCorrespondingSkill();
        }
        GameManager.Instance.changeState(GameState.GameState);
    }

    public void addCorrespondingSkill(){
        switch(skill.skillName){
            case "Plow":
                PStat.plow++;
                break;
            case "Saw":
                PStat.saw++;
                break;
            case "HP Up":
                PStat.hpUp++;
                break;
            case "Def Up":
                PStat.defUp++;
                break;
            case "Speed Up":
                PStat.spdUp++;
                break;
            case "Machine Gun":
                PStat.mGun.Upgrade();
                break;
            case "Shotgun":
                PStat.sGun.Upgrade();
                break;
            case "Heal":
                Debug.Log("Heal");
                PStat.FullHeal();
                break;
        }
    }
}
