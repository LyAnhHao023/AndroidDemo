using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningMap1 : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] int dmg;


    private void OnEnable()
    {
        transform.position=RandomPos();
        StartCoroutine(DeActive());
    }


    private Vector3 RandomPos()
    {
        int x = Random.Range(0, 16);
        int y= Random.Range(0, 9);

        return new Vector3(x+ player.transform.position.x, y+ player.transform.position.y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 playerGO = collision.GetComponent<CharacterInfo_1>();
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (playerGO != null)
        {
            playerGO.TakeDamage(dmg);
            return;
        }
        if (enemy != null)
        {
            bool isDead = enemy.EnemyTakeDmg(dmg);
            PostDmg(dmg, transform.position, false);
            if (isDead)
            {
                player.GetComponent<CharacterInfo_1>().KilledMonster();
            }
        }
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }
}
