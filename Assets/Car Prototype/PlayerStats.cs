using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    [SerializeField] private int currentHp;
    public int atk;
    [SerializeField] private int currentDef;
    public GameOverManager gom;


    public int currentLevel = 0;
    public int plow = 0;
    public int saw = 0;
    public int hpUp = 0;
    public int spdUp = 0;
    public int defUp = 0;
    public Slider healthUI;
    private PStat currentStat;

    [SerializeField] private PStatSO stat;
    public Turret mGun, sGun;

    private PlayerMove mov;

    private void Awake() {
        mov = GetComponent<PlayerMove>();
        OnStatChange();
        healthUI.maxValue = currentStat.mhp;
    }

    private void Start() {
        currentHp = currentStat.mhp;
        currentDef = currentStat.mdef;
        atk = currentStat.atk;
        healthUI.value = currentHp;
    }

    private void FixedUpdate() {
        if (Time.frameCount % 16 != 0) { return; }
        if (transform.position.y < -10f) {
            gom.GameEnd();
        }
    }

    public void OnStatChange() {
        currentStat = stat.GetStat(currentLevel, plow, saw, hpUp, defUp, spdUp);
        healthUI.maxValue = currentStat.mhp;
        mov.ChangeStat(currentStat.mSpeed);
    }

    public void TakeDamage(int damage) {

        if (currentDef < damage) {
            currentDef = 0;
            damage -= currentDef;
            currentHp -= damage;
            if(currentHp <= 0){
                gom.GameEnd();
            }
        } else {
            currentDef -= damage;
        }
        healthUI.value = currentHp;
    }

    public void FullHeal(){
        currentHp = currentStat.mhp;
        healthUI.value = currentStat.mhp;
    }

    public void recoverHealth(){
        currentHp += (int) Mathf.Floor(currentStat.mhp/10);
        if(currentHp > currentStat.mhp){
            currentHp = currentStat.mhp;
        }
        healthUI.value = currentHp;
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
