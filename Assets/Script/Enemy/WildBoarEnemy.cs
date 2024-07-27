using Cinemachine;
using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WildBoarEnemy : EnemyBase
{
    public GameObject targetGameObject;

    float timeAttack;
    float timeApllyDmg;

    Animator animator;

    int rotasionChange = 0;

    [SerializeField]
    GameObject HealthPrefab;
    [SerializeField]
    GameObject ExpGreenPrefab;
    [SerializeField]
    GameObject ExpRedPrefab;
    [SerializeField]
    GameObject CoinPrefab;
    [SerializeField]
    GameObject ChestPrefab;

    GameObject ParentDropItem;
    Rigidbody2D rb;

    bool isUseSkill=false;

    [SerializeField] bool isBoss=false;

    AudioManager audioManager;

    CinemachineVirtualCamera camera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        camera = GameObject.FindGameObjectWithTag("VirturalCamera").GetComponent<CinemachineVirtualCamera>();
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;

        if (gameObject.GetComponent<AIPath>() != null)
        {
            GetComponent<AIPath>().maxSpeed = enemyStats.speed;
            GetComponent<AIDestinationSetter>().SetTarget(targetGameObject);
        }
        else
        {
            gameObject.GetComponent<EnemyMove>().SetData(targetGameObject.transform, enemyStats);
        }
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    private void FixedUpdate()
    {
        timeApllyDmg -= Time.deltaTime;
        if (!isUseSkill)
        {
            rotasionChange = transform.position.x > targetGameObject.transform.position.x ? 180 : 0;
            animator.transform.rotation = Quaternion.Euler(0, rotasionChange, 0);
        }

        timeAttack -= Time.deltaTime;
        Collider2D[] collider = Physics2D.OverlapBoxAll(transform.position, new Vector2(18, 18), 0f);
        foreach (var item in collider)
        {
            if (item.gameObject == targetGameObject && timeAttack<0)
            {
                timeAttack = enemyStats.timeAttack;
                ReadyRush();
                return;
            }
        }

    }

    private void ReadyRush()
    {
        audioManager.PlaySFX(audioManager.WildBoar);
        isUseSkill = true;
        if(GetComponent<AIPath>()!=null)
        {
            GetComponent<AIPath>().canMove = false;
        }
        else
        {
            GetComponent<EnemyMove>().canMove = false;
        }
        GetComponent<Collider2D>().isTrigger = true;
        animator.SetBool("Skill", true);
        StartCoroutine(DelayedAction(0.9f, Rush));
    }

    private void Rush()
    {
        isKnockback = true;
        Vector2 dic=targetGameObject.transform.position-transform.position;
        rb.AddForce(dic*50,ForceMode2D.Force);
        StartCoroutine(DelayedAction(2f, StopRush));
    }

    private void StopRush()
    {
        isUseSkill = false;
        isKnockback = false;
        if (GetComponent<AIPath>() != null)
        {
            GetComponent<AIPath>().canMove = true;
        }
        else
        {
            GetComponent<EnemyMove>().canMove = true;
        }
        GetComponent<Collider2D>().isTrigger = false;
        animator.SetBool("Skill", false);
        rb.velocity = Vector2.zero;
    }

    IEnumerator DelayedAction(float timeDelay, Action function)
    {
        yield return new WaitForSeconds(timeDelay);
        function();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject && timeApllyDmg <= 0)
        {
            Attack();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == targetGameObject && timeApllyDmg <= 0)
        {
            Attack();
        }else if (collision.gameObject.layer == 6)
        {
            StopRush();
        }
    }

    private void Attack()
    {
        StopRush();
        timeApllyDmg = 1f;  
        targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
    }

    private void DestroyOb()
    {
        Destroy(gameObject, 1f);
        Drop();
    }

    private void Drop()
    {
        if (UnityEngine.Random.value * 100 <= enemyStats.chanceDropHeath)
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

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);

            if (enemyData.isBoss)
            {
                audioManager.PlaySFX(audioManager.BossDead);
                CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                _cbmcp.m_AmplitudeGain = 2f;
                StartCoroutine(StopShake());
            }

            DestroyOb();
            return true;
        }
        return false;
    }

    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.5f);
        CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;
    }
}
