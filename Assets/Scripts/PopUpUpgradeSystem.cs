using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpUpgradeSystem : MonoBehaviour
{
    public GameObject upgradeUI;
    public void NextLevel(){
        upgradeUI.SetActive(true);
    }
}
