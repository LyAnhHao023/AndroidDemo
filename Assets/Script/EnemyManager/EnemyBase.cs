using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public EnemyData enemyData;

    public EnenmyStats enemyStats;

    public bool isKnockback=false;
    private Rigidbody2D rb;

    
    public virtual void SetData(EnemyData enemy)
    {
        enemyData = enemy;
        enemyStats = new EnenmyStats(enemy.stats.hp, enemy.stats.dmg, enemy.stats.speed, enemy.stats.timeAttack,
                                     enemy.stats.chanceDropCoin,enemy.stats.chanceDropHeath,enemy.stats.chanceDropExp);
        rb=GetComponent<Rigidbody2D>();
    }

    public abstract void SetTarget(GameObject GameObject);
    public abstract void SetParentDropItem(GameObject gameObject);

    public void StatsPlus(EnenmyStats enemyStats)
    {
        if (enemyStats == null) { return; }
        this.enemyStats.hp += enemyStats.hp;
        this.enemyStats.dmg += enemyStats.dmg;
        this.enemyStats.speed += enemyStats.speed;
        this.enemyStats.timeAttack += enemyStats.timeAttack;
        this.enemyStats.chanceDropCoin += enemyStats.chanceDropCoin;
        this.enemyStats.chanceDropExp += enemyStats.chanceDropExp;
        this.enemyStats.chanceDropHeath += enemyStats.chanceDropHeath;
    }

    public void StatsBuffByTime(float persentBuff)
    {
        this.enemyStats.hp =(int)(this.enemyStats.hp* persentBuff);
        this.enemyStats.dmg = (int)(this.enemyStats.dmg * persentBuff);
        this.enemyStats.speed = (int)(this.enemyStats.speed * persentBuff);
        this.enemyStats.timeAttack = (this.enemyStats.timeAttack * persentBuff);
    }

    public void IncreaseDecreaseSpeed(int speed)
    {
        enemyStats.speed += speed;
        AIPath aIPath = gameObject.GetComponent<AIPath>();
        if (aIPath != null)
        {
            aIPath.maxSpeed += speed;
        }
    }
    public abstract bool EnemyTakeDmg(int dmg);

    public void Knockback(Vector2 direction, float force)
    {
        if (!isKnockback&& !GetComponent<Collider2D>().isTrigger)
        {
            isKnockback = true;
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            rb.AddForce(direction * force, ForceMode2D.Impulse);

            // Sau một khoảng thời gian, reset lại trạng thái isKnockback
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(0.5f);
        if (GetComponent<AIPath>() != null&& enemyStats.hp > 0)
        {
            GetComponent<AIPath>().canMove = true;
        }
        else if(enemyStats.hp>0)
        {
            GetComponent<EnemyMove>().canMove = true;
        }
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        isKnockback = false;

    }

}
