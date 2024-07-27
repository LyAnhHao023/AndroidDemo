using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonEnemy : EnemyBase
{
    public GameObject targetGameObject;

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
    public override bool EnemyTakeDmg(int dmg) => throw new System.NotImplementedException();

    private void Awake()
    {
        Destroy(gameObject,50f);
        AudioManager audioManager= GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySFX(audioManager.Warning);
    }

    public void DestroyParent()
    {
        Destroy(gameObject);
    }

    public void Attack()
    {
        targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(enemyStats.dmg);
    }

    public override void SetParentDropItem(GameObject gameObject)
    {
        ParentDropItem = gameObject;
    }

    public override void SetTarget(GameObject GameObject)
    {
        targetGameObject = GameObject;
        transform.position = targetGameObject.transform.position;
    }

    public void Drop(Vector3 posDrop)
    {
        if (Random.value * 100 <= enemyStats.chanceDropHeath)
        {
            Transform health = Instantiate(HealthPrefab).transform;
            health.position = posDrop;
            health.transform.parent = ParentDropItem.transform;
        }

        //Transform chest = Instantiate(ChestPrefab).transform;
        //chest.position = transform.position;

        if (Random.value * 100 <= enemyStats.chanceDropExp)
        {
            GameObject createExpRed = Instantiate(ExpRedPrefab);
            createExpRed.transform.position = posDrop;
            createExpRed.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createExpRed.transform.parent = ParentDropItem.transform;
        }
        else
        {
            GameObject createGreen = Instantiate(ExpGreenPrefab);
            createGreen.transform.position = posDrop;
            createGreen.GetComponent<CapsuleExp>().SetPlayer(targetGameObject);
            createGreen.transform.parent = ParentDropItem.transform;
        }

        if (Random.value * 100 <= enemyStats.chanceDropCoin)
        {
            GameObject createCoins = Instantiate(CoinPrefab);
            createCoins.transform.position = posDrop;
            createCoins.GetComponent<CoinScript>().SetPlayer(targetGameObject);
            createCoins.transform.parent = ParentDropItem.transform;
        }
    }
}
