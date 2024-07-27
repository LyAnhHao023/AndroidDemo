using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChlldrenZombieSurround : EnemyBase
{
    int hp;
    Animator animator;
    [SerializeField]
    Transform target;
    [SerializeField]
    ZombieSurroundEnemy parentPrefab;

    public float detectionRange = 5f; // Khoảng cách gần vị trí mục tiêu

    float timer;

    Rigidbody2D rb;


    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Vector3 direction = (target.position - transform.position).normalized;
        rb.AddForce(direction*30f, (ForceMode2D)ForceMode.Force);
        hp = parentPrefab.enemyStats.hp * 5 / 100;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        // Lấy vị trí hiện tại của quái vật
        Vector3 currentPosition = transform.position;

        // Tính khoảng cách từ vị trí hiện tại đến vị trí mục tiêu
        float distance = Vector3.Distance(currentPosition, target.position);

        // Kiểm tra nếu quái vật đã đến gần vị trí mục tiêu
        if (distance <= detectionRange)
        {
            Dead();
            parentPrefab.DestroyParent();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject == parentPrefab.targetGameObject&&timer<=0)
        {
            timer = parentPrefab.enemyStats.timeAttack;
            parentPrefab.Attack();
        }
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        parentPrefab.enemyStats.hp -= dmg;
        hp -= dmg;
        animator.SetTrigger("Hit");
        if (parentPrefab.enemyStats.hp <= 0)
        {
            parentPrefab.DestroyParent();
            animator.SetBool("Dead", true);
            rb.velocity = Vector3.zero;
            return true;
        }
        else if(hp<=0)
        {
            Dead();
            parentPrefab.Drop(transform.position);
            return true;
        }
        return false;
    }

    public void Dead()
    {
        Destroy(gameObject, 1f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        animator.SetBool("Dead", true);
        rb.velocity = Vector3.zero;
    }

    public override void SetTarget(GameObject GameObject)
    {
        throw new System.NotImplementedException();
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
