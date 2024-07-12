using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrop : EnemyBase
{
    [SerializeField] GameObject EffectPrefab;
    [SerializeField] GameObject RendererPrefab;

    [SerializeField] int numberDropCoins;
    [SerializeField] int numberDropExpGreen;

    [SerializeField]
    GameObject HealthPrefab;
    [SerializeField]
    GameObject ExpGreenPrefab;
    [SerializeField]
    GameObject ExpRedPrefab;
    [SerializeField]
    GameObject CoinPrefab;

    GameObject ParentDropItem;

    GameObject targetGameObject;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(AutoDesTroy());
    }

    public override bool EnemyTakeDmg(int dmg)
    {
        enemyStats.hp -= dmg;
        if (enemyStats.hp <= 0)
        {
            RendererPrefab.SetActive(false);
            EffectPrefab.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            Drop();
            Destroy(gameObject,1f);
            return false;
        }
        animator.enabled = true;
        StartCoroutine(StopAnimator());
        return false;
        
    }

    private IEnumerator StopAnimator()
    {
        yield return new WaitForSeconds(0.06f);
        animator.enabled = false;
    }

    private IEnumerator AutoDesTroy()
    {
        yield return new WaitForSeconds(30f);
        RendererPrefab.SetActive(false);
        EffectPrefab.SetActive(true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void Drop()
    { 

        if (Random.value * 100 <= enemyStats.chanceDropHeath)
        {
            Transform health = Instantiate(HealthPrefab).transform;
            health.position = RandomPositionDrops(transform.position);
            health.transform.parent = ParentDropItem.transform;
        }


        if (Random.value * 100 <= enemyStats.chanceDropExp)
        {
            GameObject createExpRed = Instantiate(ExpRedPrefab);
            createExpRed.transform.position = RandomPositionDrops(transform.position);
            createExpRed.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createExpRed.transform.parent = ParentDropItem.transform;
        }
        else
        {
            for (int i = 0; i <=Random.value*numberDropExpGreen+1; i++)
            {
                GameObject createExpRed = Instantiate(ExpGreenPrefab);
                createExpRed.transform.position = RandomPositionDrops(transform.position);
                createExpRed.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
                createExpRed.transform.parent = ParentDropItem.transform;
            }
        }

        if (Random.value * 100 <= enemyStats.chanceDropCoin)
        {
            for (int i = 0; i <= Random.value*numberDropCoins+1; i++)
            {
                GameObject createCoins = Instantiate(CoinPrefab);
                createCoins.transform.position = RandomPositionDrops(transform.position);
                createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
                createCoins.transform.parent = ParentDropItem.transform;
            }
        }
    }

    public Vector3 RandomPositionDrops(Vector3 center)
    {
        float radius = 3f;
        // Tạo một vector direction random
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        // Thêm vector direction vào vị trí trung tâm
        Vector3 randomPosition = center + randomDirection;

        return randomPosition;
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject=GameObject;
    }
}
