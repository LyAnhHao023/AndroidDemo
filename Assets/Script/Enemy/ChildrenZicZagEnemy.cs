using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenZicZagEnemy : EnemyBase
{
    int hp;
    Vector3 target;
    float step = 0;
    bool isMoveDown=false;
    [SerializeField]
    ZicZagEnemy parentPrefab;
    Animator animator;

    float timer = 0;

    private void Start()
    {
        hp=parentPrefab.enemyStats.hp;
        target= transform.position*-1;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        timer-=Time.deltaTime;
        MoveToTarget();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==parentPrefab.targetGameObject&&timer<=0)
        {
            timer = parentPrefab.enemyStats.timeAttack;
            parentPrefab.Attack();
        }
    }

    private void MoveToTarget()
    {
        if (step > 4)
        {
            isMoveDown = true;
        }
        else if(step < -4)
        {
            isMoveDown = false;
        }
        step = isMoveDown ? step - 0.01f : step + 0.01f;
        // Tính toán hướng di chuyển
        Vector3 direction = (target - transform.position).normalized;
        // Di chuyển quái theo hướng đến vị trí đích với tốc độ nhất định
        Vector3 m=transform.position + direction * parentPrefab.enemyStats.speed * Time.deltaTime;
        //Di chuyển lên hoặc xuống
        m.y=target.y + step;
        transform.position = m;
        // Kiểm tra xem quái đã đến vị trí đích chưa
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            parentPrefab.DestroyParent();
        }
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        hp-=dmg;
        animator.SetTrigger("Hit");
        if (hp < 0)
        {
            animator.SetBool("Dead", true);
            Destroy(gameObject, 0.5f);
            parentPrefab.Drop(transform.position);
            return true;
        }
        return false;
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
