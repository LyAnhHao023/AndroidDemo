using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShostEnemyScipt : EnemyBase
{
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    float timeAttack;

    Animator animator;

    [SerializeField]
    GameObject HealthPrefab;
    [SerializeField]
    GameObject ChestPrefab;
    [SerializeField]
    GameObject ExpRedPrefab;
    [SerializeField]
    GameObject CoinPrefab;
    [SerializeField]
    GameObject WarningPointerPrefab;

    GameObject ParentDropItem;

    [SerializeField]
    int numberDropCoins = 10;
    [SerializeField]
    int numberDropExp = 5;


    private void Awake()
    {
        animator=GetComponent<Animator>();
       
        Destroy(gameObject, 11f);
        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.Warning);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject)
        {
            targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
        }
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            DestroyEnemy();
            return true;
        }
        return false;
    }

    private void DestroyEnemy()
    {

        Destroy(gameObject, 0.8f);
        ChanceDrop();
    }

    private void ChanceDrop()
    {

        Transform health = Instantiate(HealthPrefab).transform;
        health.position = RandomPositionDrops(transform.position);
        health.transform.parent = ParentDropItem.transform;

        //Transform chest = Instantiate(ChestPrefab).transform;
        //chest.position = RandomPositionDrops(transform.position);

        for (int i = 0; i <= numberDropCoins; i++)
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

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameObject warningPointer = Instantiate(WarningPointerPrefab);

        warningPointer.GetComponent<WarningPointer>().SetTargetAndTimeLife(transform, 2.5f);
        warningPointer.transform.parent = transform;

        Invoke("Move", 3f);
        
    }

    private void Move()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        Vector2 lookDir = targetGameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * enemyStats.speed, ForceMode2D.Impulse);
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }
}
