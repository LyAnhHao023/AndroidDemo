using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BurnEnemy : MonoBehaviour
{
    float timeToBurn;
    int dmgBurn;
    float timer;

    CharacterInfo_1 player;

    EnemyBase enemyBase;
    void Start()
    {
        enemyBase= GetComponentInParent<EnemyBase>();

    }

    public void SetUp(CharacterInfo_1 player, float timeToBurn, int dmgBurn)
    {
        this.player = player;
        this.timeToBurn = timeToBurn;
        this.dmgBurn = dmgBurn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer-=Time.deltaTime;
        if(timer < 0)
        {
            timer = timeToBurn;
            bool isDead= enemyBase.EnemyTakeDmg(dmgBurn);
            PostDmg(dmgBurn, transform.position, false);
            if (isDead)
            {
                player.KilledMonster();
            }
        }
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }
}
