using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "SkillSO")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public string category;
    public int level;
    public int maxLevel;
    public bool isLoop = false;
}
