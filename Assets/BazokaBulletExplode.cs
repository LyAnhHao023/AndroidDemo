using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BazokaBulletExplode : MonoBehaviour
{
    int dmgBullet = 2;
    bool isCrit;

    public void SetDmg(int dmg, bool isCrit)
    {
        dmgBullet = dmg;
        this.isCrit = isCrit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            MessengerSystem.instance.DmgPopUp(dmgBullet.ToString(), enemy.transform.position, isCrit);
            bool isDead = enemy.EnemyTakeDmg(dmgBullet);
            if (isDead)
            {
                GameObject.Find("Player").GetComponent<CharacterInfo_1>().KilledMonster();
            }

        }
    }
}
