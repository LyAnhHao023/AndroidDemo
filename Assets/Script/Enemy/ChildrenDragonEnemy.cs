using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenDragonEnemy : EnemyBase
{
    Animator animator;
    [SerializeField]
    DragonEnemy parentPrefab;

    float timer;

    Rigidbody2D rb;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 dic=new Vector3(parentPrefab.transform.position.x-transform.position.x>0?1:-1,0,0);
        rb.AddForce(dic*100f, (ForceMode2D)ForceMode.Force);
        SetData(parentPrefab.enemyData);
        enemyStats.hp = parentPrefab.enemyStats.hp;
    }

    private void Update()
    {
        timer-=Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==parentPrefab.targetGameObject&&timer<=0)
        {
            timer = parentPrefab.enemyStats.timeAttack;
            parentPrefab.targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(parentPrefab.enemyStats.dmg);
        }
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0) 
        {
            Dead();
            return true;
        }
        return false;
    }

    private void Dead()
    {
        animator.SetBool("Dead", true);
        rb.velocity = Vector3.zero;
        GetComponent<Collider2D>().enabled = false;
        parentPrefab.Drop(transform.position);
        Destroy(gameObject, 1f);
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public override void SetTarget(GameObject GameObject)
    {
        throw new System.NotImplementedException();
    }
}
