using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int currentLevel = 0;
    private PStat currentStat, cUpgradeStat;

    [SerializeField] private PStat[] statEachLevel;

    public void OnLevelUp(int lv) {
        if (lv < 0 || lv >= statEachLevel.Length) { return; }
        currentLevel = lv;
        currentStat = statEachLevel[lv] + cUpgradeStat;
    }

    //NOTE THIS IS ADDITIVE
    public void OnUpgrade(PStat upStat) {
        cUpgradeStat += upStat;
        OnLevelUp(currentLevel);
    }
}

[System.Serializable]
public struct PStat {
    public int mhp;
    public int mdef;
    public int mSpeed;
    public int atk;

    public static PStat operator +(PStat a, PStat b) {
        return new PStat {
            mhp = a.mhp + b.mhp,
            mdef = a.mdef + b.mdef,
            mSpeed = a.mSpeed + b.mSpeed,
            atk = a.atk + b.atk
        };
    }
}
