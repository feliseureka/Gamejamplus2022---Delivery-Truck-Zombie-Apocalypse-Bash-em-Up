using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpUpgradeSystem : MonoBehaviour
{
    public GameObject upgradeUI;
    private Button leftUpgrade;
    private Button rightUpgrade;

    void Start(){
        leftUpgrade = upgradeUI.transform.GetChild(1).GetComponent<Button>();
        rightUpgrade = upgradeUI.transform.GetChild(2).GetComponent<Button>();
        GameManager.OnStateChanged += GameOnStateChanged;
    }

    void Destroy(){
        GameManager.OnStateChanged -= GameOnStateChanged;
    }

    public void GameOnStateChanged(GameState state){
        if(GameManager.Instance.state == GameState.UpgradeState){
            NextLevel();
        }
    }

    public void NextLevel(){
        upgradeUI.SetActive(true);
    }
}
