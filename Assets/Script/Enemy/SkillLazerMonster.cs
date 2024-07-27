using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLazerMonster : MonoBehaviour
{
    [SerializeField]
    LazerMonster lazerMonsterPrefab;

    private void OnEnable()
    {
        Invoke("DeActive", 1f);
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 player= collision.GetComponent<CharacterInfo_1>();
        if(player!=null)
        {
            player.TakeDamage(lazerMonsterPrefab.enemyStats.dmg/3);
        }
    }
}
