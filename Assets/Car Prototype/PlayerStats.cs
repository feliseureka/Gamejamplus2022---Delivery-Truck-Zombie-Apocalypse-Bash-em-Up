using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private int currentLevel = 0;
    private int plow = 0;
    private int saw = 0;
    private int hpUp = 0;
    private int spdUp = 0;
    private int defUp = 0;
    private PStat currentStat;

    [SerializeField] private PStatSO stat;

    private PlayerMove mov;

    private void Awake() {
        mov = GetComponent<PlayerMove>();
    }

    public void OnStatChange() {
        currentStat = stat.GetStat(currentLevel, plow, saw, hpUp, defUp, spdUp);
        mov.ChangeStat(currentStat.mSpeed);
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

    public static PStat operator *(int f, PStat a) {
        return new PStat {
            mhp = a.mhp * f,
            mdef = a.mdef * f,
            mSpeed = a.mSpeed * f,
            atk = a.atk * f
        };
    }
}
