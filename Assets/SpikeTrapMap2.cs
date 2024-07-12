using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapMap2 : MonoBehaviour
{
    [SerializeField] int dmg = 5;

    [SerializeField] float timeApllyDmg = 0.5f;

    [SerializeField] CharacterInfo_1 player;

    float timer;

    private void Update()
    {
        timer -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterInfo_1 playerGO = collision.GetComponent<CharacterInfo_1>();
        
        if (playerGO != null && timer<=0)
        {
            timer = timeApllyDmg;
            playerGO.TakeDamage(dmg);
            return;
        }

        EnemyBase enemy = collision.GetComponent<EnemyBase>();

        if (enemy != null && timer <= 0)
        {
            timer = timeApllyDmg;
            bool isDead = enemy.EnemyTakeDmg(dmg);
            PostDmg(dmg, transform.position, false);
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
