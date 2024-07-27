using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicanEnemyScript : EnemyBase
{
    [SerializeField]
    //Nhận biết tấn công player 
    public GameObject targetGameObject;

    [SerializeField]
    Transform firePos;
    [SerializeField]
    GameObject SkillPrefab;

    float timer;

    Animator animator;

    int rotasionChange = 0;

    [SerializeField]
    Vector2 areaAttack = new Vector2(3, 3);

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
        rotasionChange = transform.position.x > targetGameObject.transform.position.x ? 0 : 180;
        animator.transform.rotation = Quaternion.Euler(0, rotasionChange, 0);

        Collider2D[] Collider2D = Physics2D.OverlapBoxAll(transform.position, areaAttack, 0f);
        GameObject targetObject = Collider2D.Where(c => c.gameObject == targetGameObject).FirstOrDefault()?.gameObject;
        if(targetObject != null)
        {
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            animator.SetBool("isReady", true);
            timer -=Time.deltaTime;
            if(timer < 0)
            {
                timer = enemyStats.timeAttack;
                Attack();
            }

        }
        else
        {
            if (GetComponent<AIPath>() != null)
            {
                GetComponent<AIPath>().canMove = true;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = true;
            }
            animator.SetBool("isReady", false);
        }

    }

    private void Attack()
    {
        
        GameObject createSkill = Instantiate(SkillPrefab, firePos.position, Quaternion.identity);
        createSkill.transform.parent = GameObject.Find("===ObjectDrop===").transform;
        createSkill.GetComponent<MagicicanAttackScript>().SetDmg(enemyStats.dmg);

        Rigidbody2D rb = createSkill.GetComponent<Rigidbody2D>();
        Vector3 direction = (targetGameObject.transform.position - firePos.position).normalized;
        rb.AddForce(direction * 10f, (ForceMode2D)ForceMode.Impulse);
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
                GetComponent<AIPath>().canMove = false;
            }
            else
            {
                GetComponent<EnemyMove>().canMove = false;
            }
            animator.SetBool("Dead", true);
            Destroy(gameObject,1f);
            Drop();
            return true;
        }
        return false;
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
