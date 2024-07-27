using Cinemachine;
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinotaur : EnemyBase
{
    [SerializeField]
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    [SerializeField]
    GameObject slashSkillPrefab;
    [SerializeField] Transform Skill1Pos;

    [SerializeField]
    GameObject Skill1;

    [SerializeField] List<GameObject> Skill2Lst = new List<GameObject>();



    float timer;
    [SerializeField]
    float timeSkill1 = 6;
    [SerializeField]
    float timeSkill2 = 9;

    float timerSkill1;
    float timerSkill2;

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

    [SerializeField]
    int numberDropCoins;
    [SerializeField]
    int numberDropExp;

    GameObject ParentDropItem;

    bool isUseSkill=false;

    private CinemachineVirtualCamera camera;

    private AudioManager audioManager;

    [SerializeField]
    AudioClip shakeSound;
    [SerializeField]
    AudioClip slashSound;

    GameObject mainMenu;
    MenuManager menuManager;

    CharacterInfo_1 characterInfo;

    private void Awake()
    {
        mainMenu = GameObject.FindGameObjectWithTag("MenuManager");
        menuManager = mainMenu.GetComponent<MenuManager>();
        characterInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo_1>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        timerSkill1 = timeSkill1;
        timerSkill2 = timeSkill2;

        camera = GameObject.FindGameObjectWithTag("VirturalCamera").GetComponent<CinemachineVirtualCamera>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.SetBackGround(audioManager.BossFight);

    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = transform.position.x > targetGameObject.transform.position.x;

        timer -=Time.deltaTime;
        timerSkill1 -= Time.deltaTime;
        timerSkill2 -= Time.deltaTime;

        if( timerSkill1 <= 0&&!isUseSkill)
        {
            timerSkill1 = timeSkill1;
            StartCoroutine(SkillOne());
        }
        else if( timerSkill2 <= 0&& !isUseSkill) 
        {
            timerSkill2 = timeSkill2;
            StartCoroutine(SkillTwo());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CharacterInfo_1 player=collision.GetComponent<CharacterInfo_1>();

        if (timer <= 0&& player!=null)
        {
            timer = enemyStats.timeAttack;
            player.TakeDamage(enemyStats.dmg);
        }
    }

    private IEnumerator SkillOne()
    {
        audioManager.PlaySFX(slashSound);
        animator.SetBool("Skill1", true);
        isUseSkill = true;
        yield return new WaitForSeconds(0.3f);
        Vector2 lookDir = targetGameObject.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Skill1.transform.rotation = Quaternion.Euler(0, 0, angle);

        GameObject slash= Instantiate(slashSkillPrefab);
        slash.GetComponent<SlashSkill1MinotairBoss>().SetTargetAndDmg(targetGameObject,enemyStats.dmg);
        slash.transform.position = Skill1Pos.position;
        slash.transform.rotation=Skill1.transform.rotation;
        Rigidbody2D rb= slash.GetComponent<Rigidbody2D>();
        rb.AddForce(slash.transform.right * 9f, ForceMode2D.Impulse);
        Destroy(slash, 5f);
        animator.SetBool("Skill1", false);
        isUseSkill = false;
    }

    private IEnumerator SkillTwo()
    {
        GetComponent<AIPath>().canMove = false;
        animator.SetBool("Skill2", true);
        isUseSkill=true;
        yield return new WaitForSeconds(1.2f);

        audioManager.PlaySFX(shakeSound);
        //Shake Camera
        CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 2f;
        StartCoroutine(StopShake());

        
        yield return new WaitForSeconds(0.1f);

        GetComponent<AIPath>().canMove = true;
        animator.SetBool("Skill2", false);
        isUseSkill = false;
    }

    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(0.5f);
        CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;

        foreach (var item in Skill2Lst)
        {
            item.SetActive(true);
        }
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        gameObject.GetComponent<AIPath>().maxSpeed = enemyStats.speed;
        SetTargetSkill2();
        GetComponent<AIDestinationSetter>().SetTarget(targetGameObject);
    }

    private void SetTargetSkill2()
    {
        foreach (var item in Skill2Lst)
        {
            item.GetComponent<RockFallingMap3>().SetPlayerTarget(targetGameObject);
        }
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        animator.SetTrigger("Hit");
        if (enemyStats.hp <= 0)
        {
            StaticData.bigBossKill++;
            GetComponent<AIPath>().canMove = false;
            GetComponent<Collider2D>().enabled = false;
            animator.SetBool("Dead", true);

            if (StaticData.MapSelect != null)
            {
                if (StaticData.LevelType == 0)
                {
                    characterInfo.MissionCheck();
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

            Destroy(gameObject, 1f);

            audioManager.PlaySFX(audioManager.BossDead);

            CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cbmcp.m_AmplitudeGain = 2f;
            StartCoroutine(StopShake());

            Drop();
            return true;
        }
        return false;
    }

    private void Drop()
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
}
