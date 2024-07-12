using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AchievementType
{
    Complete = 0,
    Count = 1,
}

[CreateAssetMenu]
public class AchievementData : ScriptableObject
{
    public AchievementType type;
    public Sprite icon;
    public string achieName;
    public string description;
    public string key;
    public int num;
    public int points;
    public bool complete = false;
}
