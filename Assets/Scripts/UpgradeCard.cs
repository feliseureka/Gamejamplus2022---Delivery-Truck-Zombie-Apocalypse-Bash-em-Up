using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    public string nameUpgrade;
    public string desc;

    public void onCardClick(){
        transform.parent.gameObject.SetActive(false);
        GameManager.Instance.changeState(GameState.GameState);
    }
}
