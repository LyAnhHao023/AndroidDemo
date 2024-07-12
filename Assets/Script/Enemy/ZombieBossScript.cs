using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBossScript : EnemyBase
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
    GameObject ExpRedPrefab;
    [SerializeField]
    GameObject CoinPrefab;

    GameObject ParentDropItem;

    [SerializeField]
    int numberDropCoins=10;
    [SerializeField]
    int numberDropExp=5;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        if (GetComponent<AIPath>() != null)
        {
            GetComponent<AIPath>().maxSpeed = enemyStats.speed;
            GetComponent<AIDestinationSetter>().SetTarget(targetGameObject);
        }
        else
        {
            GetComponent<EnemyMove>().SetData(targetGameObject.transform, enemyStats);
        }
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    private void Update()
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

    private void DestroyZombie()
    {
        Destroy(gameObject, 1f);
        ChanceDrop();
    }

    private void ChanceDrop()
    {

        Transform health = Instantiate(HealthPrefab).transform;
        health.position = RandomPositionDrops(transform.position);
        health.transform.parent = ParentDropItem.transform;

        Transform chest = Instantiate(ChestPrefab).transform;
        chest.position = RandomPositionDrops(transform.position);

        for (int i=0;i<=numberDropCoins;i++)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = RandomPositionDrops(transform.position);
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
        }
        for (int i = 0; i <= numberDropExp; i++)
        {
            GameObject createExpRed = Instantiate(ExpRedPrefab);
            createExpRed.transform.position = RandomPositionDrops(transform.position);
            createExpRed.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createExpRed.transform.parent = ParentDropItem.transform;
        }
    }

    public Vector3 RandomPositionDrops(Vector3 center)
    {
         float radius = 5f;
        // Tạo một vector direction random
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        // Thêm vector direction vào vị trí trung tâm
        Vector3 randomPosition = center + randomDirection;

        return randomPosition;
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            if (GetComponent<AIPath>() != null)
            {
                gameObject.GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            animator.SetBool("Dead", true);
            DestroyZombie();
            return true;
        }
        return false;
    }
}
