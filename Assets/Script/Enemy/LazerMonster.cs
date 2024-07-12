using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMonster : EnemyBase
{
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
    int numberDropCoins = 10;
    [SerializeField]
    int numberDropExp = 5;

    [SerializeField]
    GameObject LazerSkillPrefab;

    float timerSkillLazer;

    GameObject mainMenu;
    MenuManager menuManager;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        mainMenu = GameObject.FindGameObjectWithTag("MenuManager");
        menuManager = mainMenu.GetComponent<MenuManager>();

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

        timerSkillLazer-= Time.deltaTime;
        if(timerSkillLazer < 0)
        {
            timerSkillLazer = 4f;
            LazerSkillPrefab.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
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

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            StaticData.bigBossKill++;
            gameObject.GetComponent<AIPath>().canMove = false;
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);
            DestroyOb();

            if(StaticData.MapSelect != null)
            {
                if (StaticData.LevelType == 0)
                {
                    menuManager.GameOverScreen(true);
                    PlayerPrefs.SetInt("Stage" + StaticData.MapSelect.key, 1);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetInt(StaticData.MapSelect.key, 1);
                    PlayerPrefs.Save();
                }

                if (StaticData.LevelType == 2 && StaticData.bigBossKill >= 3)
                {
                    menuManager.GameOverScreen(true);
                    PlayerPrefs.SetInt("Challange" + StaticData.MapSelect.key, 1);
                    PlayerPrefs.Save();
                }
            }

            return true;
        }
        return false;
    }
}
