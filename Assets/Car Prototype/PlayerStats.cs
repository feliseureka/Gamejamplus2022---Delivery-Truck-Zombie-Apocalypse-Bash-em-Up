using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField] private int currentHp;
    [SerializeField] private int atk;
    [SerializeField] private int currentDef;


    public int currentLevel = 0;
    public int plow = 0;
    public int saw = 0;
    public int hpUp = 0;
    public int spdUp = 0;
    public int defUp = 0;
    private PStat currentStat;

    [SerializeField] private PStatSO stat;
    public Turret mGun, sGun;

    private PlayerMove mov;

    private void Awake() {
        mov = GetComponent<PlayerMove>();
        OnStatChange();
    }

    private void Start() {
        currentHp = currentStat.mhp;
        currentDef = currentStat.mdef;
        atk = currentStat.atk;
    }

    //TESSS
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            spdUp++;
            OnStatChange();
        }
    }

    public void OnStatChange() {
        currentStat = stat.GetStat(currentLevel, plow, saw, hpUp, defUp, spdUp);
        mov.ChangeStat(currentStat.mSpeed);
    }

    public void TakeDamage(int damage) {

        if (currentDef < damage) {
            currentDef = 0;
            damage -= currentDef;
            currentHp -= damage;
        } else {
            currentDef -= damage;
        }
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
