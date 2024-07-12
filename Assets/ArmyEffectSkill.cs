using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyEffectSkill : MonoBehaviour
{
    [SerializeField] GameObject BloodPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy= collision.GetComponent<EnemyBase>();
        if(enemy != null&&(enemy.enemyData!=null? !enemy.enemyData.isBoss:true))
        {
            enemy.EnemyTakeDmg(enemy.enemyStats.hp);
            MessengerSystem.instance.DmgPopUp(enemy.enemyStats.hp.ToString(), enemy.transform.position, true);
            GameObject bloodPrefab= Instantiate(BloodPrefab, enemy.transform);
            bloodPrefab.GetComponent<SpriteRenderer>().flipX = Random.value < 0.5f;
            bloodPrefab.transform.localScale=enemy.transform.localScale;
        }
    }
}
