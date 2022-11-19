using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "STTATT", menuName = "PSTAT")]
public class PStatSO : ScriptableObject {

    [SerializeField] private PStat[] levelStat;
    [SerializeField] private PStat[] plowStat;
    [SerializeField] private PStat[] sawStat;

    [SerializeField] private PStat hpUp, defUp, spdUp;

    public PStat GetStat(int level, int plow, int saw, int hp, int def, int spd) {
        return levelStat[level] + plowStat[plow] + sawStat[saw] + hp * hpUp + def * defUp + spd * spdUp;
    }
}
