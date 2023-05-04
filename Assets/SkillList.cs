using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillList", menuName = "ScriptableObjects/SkillList", order = 2)]
public class SkillList : ScriptableObject
{
    [SerializeField] private SkillConfig[] _skillConfigs;
    public SkillConfig[] SkillConfigs => _skillConfigs;
}
