using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum MissionType
{
    HP = 0,
    Kill = 1,
}

[Serializable]
public class MissionInfo
{
    public MissionType missionType;
    public string missionName;
    public string decription;
    public int num;
    public bool completed;
}

[CreateAssetMenu]
public class MapData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public string key;
    public bool storyCleared = false;
    public bool unlocked = false;
    public List<MissionInfo> missions;
}
