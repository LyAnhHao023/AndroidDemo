using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public int maxHealth;
    public int strenght;
    public float speed;
    public float crit;
    public float critDmg;

    public void SetStats(CharacterStats stats)
    {
        this.maxHealth = stats.maxHealth;
        this.strenght = stats.strenght;
        this.speed = stats.speed;
        this.crit = stats.crit;
        this.critDmg = stats.critDmg;
    }
}

[Serializable]
public class SkillInfo
{
    public Sprite Icon;
    public string name;
    public string decription;
    public int strenght;
    public float cdSkill;

    public void SetData(SkillInfo data)
    {
        this.Icon = data.Icon;
        this.name = data.name;
        this.decription = data.decription;
        this.strenght=data.strenght;
        this.cdSkill = data.cdSkill;
    }
}

[Serializable]
public class SkillTree
{
    //0: chưa nâng,1 chiến binh,2 pháp sư, 3 sad thủ
    public int type;
    public int level;
    public void SetData(SkillTree data)
    {
        this.type = data.type;
        this.level = data.level;
    }
}

[CreateAssetMenu]
public class CharacterData : ScriptableObject
{
    public string name;
    public Sprite image;
    public CharacterStats stats;
    public GameObject animatorPrefab;
    public WeaponData beginerWeapon;
    public SkillInfo skillInfo;
    public bool acquired = false;
    public SkillTree skillTree;
}
