using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfEnemy : EnemyBase
{
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    float timeAttack;

    Animator animator;

    int rotasionChange = 0;

    [SerializeField]
    GameObject HealthPrefab;
    [SerializeField]
    GameObject ChestPrefab;
    [SerializeField]
    GameObject ExpGreenPrefab;
    [SerializeField]
    GameObject ExpRedPrefab;
    [SerializeField]
    GameObject CoinPrefab;

    GameObject ParentDropItem;

    [SerializeField] bool isBoss=false;

    private void Awake()
    {
        animator = GetComponent<Animator>();

    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        gameObject.GetComponent<AIPath>().maxSpeed = enemyStats.speed;
        GetComponent<AIDestinationSetter>().SetTarget(targetGameObject);
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    private void FixedUpdate()
    {
        rotasionChange = transform.position.x > targetGameObject.transform.position.x ? 180 : 0;
        animator.transform.rotation = Quaternion.Euler(0, rotasionChange, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        timeAttack -= Time.deltaTime;
        if (collision.gameObject == targetGameObject && timeAttack <= 0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        timeAttack = enemyStats.timeAttack;
        targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
    }

    private void DestroyOb()
    {
        Destroy(gameObject, 0.42f);
        Drop();
    }

    private void Drop()
    {
        if (Random.value * 100 <= enemyStats.chanceDropHeath)
        {
            Transform health = Instantiate(HealthPrefab).transform;
            health.position = transform.position;
            health.transform.parent = ParentDropItem.transform;
        }

        if (isBoss)
        {
            Transform chest = Instantiate(ChestPrefab).transform;
            chest.position = transform.position;
        }

        if (Random.value * 100 <= enemyStats.chanceDropExp)
        {
            GameObject createExpRed = Instantiate(ExpRedPrefab);
            createExpRed.transform.position = transform.position;
            createExpRed.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createExpRed.transform.parent = ParentDropItem.transform;
        }
        else
        {
            GameObject createGreen = Instantiate(ExpGreenPrefab);
            createGreen.transform.position = transform.position;
            createGreen.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createGreen.transform.parent = ParentDropItem.transform;
        }

        if (Random.value * 100 <= enemyStats.chanceDropCoin)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = transform.position;
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
        }
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            gameObject.GetComponent<AIPath>().canMove = false;
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            DestroyOb();
            return true;
        }
        return false;
    }
}
