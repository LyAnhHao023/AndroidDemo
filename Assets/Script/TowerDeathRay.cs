using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerDeathRay : MonoBehaviour
{
    [SerializeField]
    float attackSpeed = 1f;
    [SerializeField]
    int dmgRay = 1;
    [SerializeField]
    Vector2 attackSize=new Vector2 (3f, 4f);
    [SerializeField]
    float timeDisableAttack = 0.2f;
    [SerializeField]
    GameObject deathRayObject;

    int randomInt;

    int[] angel= {0,90,180,270};

    float timer;

    private void Awake()
    {
        timer = attackSpeed;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Attack();
            Invoke("DisableAttack", timeDisableAttack);
        }
    }

    private void Attack()
    {
        timer =attackSpeed;
        deathRayObject.SetActive(true);
        randomInt=UnityEngine.Random.Range(0, 4);
        transform.rotation=Quaternion.Euler(randomInt == 2 ? 180f : 0f, randomInt==1?180f:0f, angel[randomInt]);
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(deathRayObject.transform.position, attackSize,0f);

        if (collider2Ds.Length > 0)
        {
            ApllyDmg(collider2Ds);
        }
        

    }

    private void DisableAttack()
    {
        deathRayObject.SetActive(false);
    }

    private void ApllyDmg(Collider2D[] collider2Ds)
    {
        for (int i = 0; i < collider2Ds.Length; i++)
        {
            playerMove player = collider2Ds[i].GetComponent<playerMove>();
            EnemyBase zombie= collider2Ds[i].GetComponent<EnemyBase>();
            if(zombie != null)
            {
               
                zombie.EnemyTakeDmg(dmgRay);
            }
            else if (player != null)
            {
                player.GetComponent<CharacterInfo_1>().TakeDamage(dmgRay);
            }
        }
    }
}
