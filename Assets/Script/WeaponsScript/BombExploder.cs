using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExploder : MonoBehaviour
{
    WeaponBase wp;
    CharacterStats characterStats;
    CharacterInfo_1 player;
   [SerializeField]
    GameObject bombPrefab;
    private void OnEnable()
    {
        player = GameObject.Find("Player").GetComponent<CharacterInfo_1>();
        characterStats = player.GetComponentInParent<CharacterInfo_1>().characterStats;
        wp = player.GetComponentInChildren<BombScript>().GetComponent<WeaponBase>();
        bombPrefab.GetComponent<Rigidbody2D>().velocity= Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            ApplyDmg(enemy);
        }
    }

    private void ApplyDmg(EnemyBase enemy)
    {
        bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;
        int dmg = (int)(isCrit ?
            (wp.weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (wp.weaponStats.dmg + characterStats.strenght));

        wp.PostDmg(dmg, enemy.transform.position, isCrit);
        bool isDead = enemy.EnemyTakeDmg(dmg);
        if (isDead)
        {
            player.GetComponentInParent<CharacterInfo_1>().KilledMonster();
        }
        Destroy(bombPrefab, 0.4f);
    }
}
