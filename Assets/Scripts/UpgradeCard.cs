using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    public SkillSO skill;
    public TMP_Text text;

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
        }
        GameManager.Instance.changeState(GameState.GameState);
    }
}
