using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBatScipt : EnemyBase
{

    [SerializeField]
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    [SerializeField]
    GameObject warningZone;
    [SerializeField]
    GameObject exploder;

    Animator animator;

    int rotasionChange = 0;

    [SerializeField]
    Vector2 areaAttack=new Vector2(3,3);

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

    AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        AIPath aIPath=GetComponent<AIPath>();
        if(aIPath != null)
        {
            aIPath.maxSpeed = enemyStats.speed;
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
        rotasionChange = transform.position.x > targetGameObject.transform.position.x ? 0 : 180;
        animator.transform.rotation = Quaternion.Euler(0, rotasionChange, 0);

        Collider2D[] Collider2D = Physics2D.OverlapBoxAll(transform.position, areaAttack, 0f);
        foreach (var item in Collider2D)
        {
            if(item.gameObject == targetGameObject&&!warningZone.active)
            {
                Attack();
            }
        }

    }

    public void ApllyDmgToPlayer()
    {
        targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
    }

    private void Attack()
    {
        GetComponent<Collider2D>().isTrigger = true;

        AIPath aIPath = GetComponent<AIPath>();
        if (aIPath != null)
        {
            GetComponent<AIPath>().canMove = false;
        }
        else
        {
            GetComponent<EnemyMove>().canMove = false;
        }
        warningZone.SetActive(true);
        Invoke("Explosion",2f);
        animator.SetBool("isReady", true);
    }

    private void Explosion()
    {
        audioManager.PlaySFX(audioManager.BoombBat);

        GetComponent<SpriteRenderer>().enabled=false;
        warningZone.SetActive(false);
        exploder.SetActive(true);
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            GetComponent<Collider2D>().enabled=false;
            DestroyBombBat();
            return true;
        }
        return false;
    }

    public void DestroyBombBat()
    {
        Destroy(gameObject,0.4f);
        Invoke("Drop", 0.4f);
    }

    private void Drop()
    {
        if (UnityEngine.Random.value * 100 <= enemyStats.chanceDropHeath)
        {
            Transform health = Instantiate(HealthPrefab).transform;
            health.position = transform.position;
            health.transform.parent = ParentDropItem.transform;
        }

        //Transform chest = Instantiate(ChestPrefab).transform;
        //chest.position = transform.position;

        if (UnityEngine.Random.value * 100 <= enemyStats.chanceDropExp)
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

        if (UnityEngine.Random.value * 100 <= enemyStats.chanceDropCoin)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = transform.position;
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
        }
    }
}
