using Cinemachine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireGirlMsScript : EnemyBase
{
    [SerializeField]
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    float timeAttack;

    Animator animator;

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

    [SerializeField]
    int HpRegeneration;

    [SerializeField] bool isBoss=false;

    CinemachineVirtualCamera camera;

    AudioManager audioManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        camera = GameObject.FindGameObjectWithTag("VirturalCamera").GetComponent<CinemachineVirtualCamera>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

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
        GetComponent<SpriteRenderer>().flipX = transform.position.x < targetGameObject.transform.position.x;
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
        animator.SetTrigger("Attack");
        timeAttack = enemyStats.timeAttack;
        targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
        //Them messenger hoi mau
        enemyStats.hp += HpRegeneration;
    }

    private void DestroyOb()
    {
        Destroy(gameObject, 0.6f);
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
            if (GetComponent<AIPath>() != null)
            {
                gameObject.GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            //Shake Camera
            if(enemyData.isBoss)
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
