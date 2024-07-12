using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GoodsType
{
    Stat = 0,
    Character = 1
}

[Serializable]
public class levelInfo
{
    public int level;
    public int price;
}

[CreateAssetMenu]
public class GoodsData : ScriptableObject
{
    public GoodsType type;
    public Sprite icon;
    public string goodsName;
    public string description;
    public int level;
    public int price;
    public int maxLevel;
    public float percentBuff;
    public List<levelInfo> levelInfos;
    public CharacterData characterData;
}
