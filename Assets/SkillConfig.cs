using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillConfig", menuName = "ScriptableObjects/SkillConfig", order = 1)]
public class SkillConfig : ScriptableObject
{
    [SerializeField] private string _name;
    public string Name => _name;
    [SerializeField] private bool _isBase;
    public bool IsBase => _isBase;
    [SerializeField] private int _cost;
    public int Cost => _cost;
    [SerializeField] private SkillConfig[] _skillsConnection;
    public SkillConfig[] SkillsConnection => _skillsConnection;
}
