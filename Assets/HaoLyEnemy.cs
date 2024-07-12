using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaoLyEnemy : EnemyBase
{

    public GameObject targetGameObject;

    float timer=20f;

    Animator animator;

    [SerializeField]
    int numberCoinDrop=40;

    [SerializeField]
    GameObject CoinPrefab;

    GameObject ParentDropItem;


    public float detectionRange = 50f;
    public int radomRange = 20;
    private Vector3 randomPos;
    bool stopMove=false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        RandomMove();
    }


    private void Update()
    {
        timer-=Time.deltaTime;

        if(timer < 0)
        {
            stopMove = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 2), Random.Range(-1, 2)) * 30, ForceMode2D.Impulse);
            Destroy(gameObject,3f);
        }

        if (Vector3.Distance(transform.position, targetGameObject.transform.position) >= detectionRange&&!stopMove)
        {
            // Di chuyển đến gần người chơi
            MoveToPlayer();
            RandomMove();
        }
        else if(!stopMove)
        {
            if(Vector3.Distance(transform.position, randomPos)>=0.4)
            {
                Vector3 direction = (randomPos - transform.position).normalized;
                transform.position += direction * enemyStats.speed * Time.deltaTime;

            }
            else
            {
                RandomMove();
                Vector3 direction = (randomPos - transform.position).normalized;
                transform.position += direction * enemyStats.speed * Time.deltaTime;
            }
        }
    }

    private void MoveToPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetGameObject.transform.position, enemyStats.speed * Time.deltaTime);
    }

    private void RandomMove()
    {
        bool flag = Random.value < 0.5f;

        randomPos=new Vector3(flag?(Random.value<0.5?Random.Range(-radomRange,-10):Random.Range(10,radomRange+1)):Random.value<0.5f?-radomRange:radomRange, 
            flag ? Random.value < 0.5f ? -radomRange : radomRange  : (Random.value < 0.5 ? Random.Range(-radomRange, -10) : Random.Range(10, radomRange + 1)), 0) + targetGameObject.transform.position;
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");

        for (int i = 0; i < 2; i++)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = RandomPositionDrops(transform.position);
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
        }

        if (enemyStats.hp <= 0)
        {
            stopMove=true;  

            ChanceDrop();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1,2), Random.Range(-1, 2))*30,ForceMode2D.Impulse);
            Destroy(gameObject,5f);
            return false;
        }
        return false;
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    private void ChanceDrop()
    {

        for (int i = 0; i <= numberCoinDrop; i++)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = RandomPositionDrops(transform.position);
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
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
}
