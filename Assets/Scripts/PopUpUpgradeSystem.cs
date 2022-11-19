using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUpgradeSystem : MonoBehaviour
{
    public static List<SkillSO> SkillArray;
    public List<SkillSO> skillArr;

    public GameObject upgradeUI;
    private UpgradeCard leftUpgrade;
    private UpgradeCard rightUpgrade;

    void Start(){
        leftUpgrade = upgradeUI.transform.GetChild(1).GetComponent<UpgradeCard>();
        rightUpgrade = upgradeUI.transform.GetChild(2).GetComponent<UpgradeCard>();
        GameManager.OnStateChanged += GameOnStateChanged;
        SkillArray = skillArr;
    }

    void EnqueueSkill(){
        int a = Random.Range(0,SkillArray.Count);
        int b = (a + Random.Range(1,SkillArray.Count - 1)) % SkillArray.Count;
        leftUpgrade.skill = SkillArray[a];
        rightUpgrade.skill = SkillArray[b];
    }

    void Destroy(){
        GameManager.OnStateChanged -= GameOnStateChanged;
    }

    public void GameOnStateChanged(GameState state){
        if(GameManager.Instance.state == GameState.UpgradeState){
            NextLevel();
        }else EnqueueSkill();
    }

    public void NextLevel(){
        Time.timeScale = 0;
        upgradeUI.SetActive(true);
    }
}
