using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnenmyStats
{
    public int hp;
    public int dmg;
    public int speed;
    public float timeAttack;
    public float chanceDropCoin;
    public float chanceDropHeath;
    public float chanceDropExp;

    public EnenmyStats(int hp, int dmg, int speed,float timeAttack, float chanceDropCoin, float chanceDropHeath, float chanceDropExp)
    {
        this.hp = hp;
        this.dmg = dmg;
        this.speed = speed;
        this.timeAttack = timeAttack;
        this.chanceDropCoin = chanceDropCoin;
        this.chanceDropHeath = chanceDropHeath;
        this.chanceDropExp = chanceDropExp;
    }
}

[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public EnenmyStats stats;
    public string name;
    public bool isBoss;
    public GameObject EnemyBasePrefab;
}
