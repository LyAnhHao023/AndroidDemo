using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public int dmg;
    public int level;
    public float timeAttack;

    public WeaponStats(int damage,int level, float timeAttack)
    {
        this.dmg = damage;
        this.level = level;
        this.timeAttack = timeAttack;
    }

    public void SetStats(WeaponStats stats)
    {
        this.dmg = stats.dmg;
        this.level = stats.level;
        this.timeAttack = stats.timeAttack;
    }
}
[CreateAssetMenu]
public class WeaponData : ScriptableObject
{
    public string name;
    public WeaponStats stats;
    public bool maxed;
    public GameObject weaponBasePrefab;
    public WeaponData weaponColabData;
}
