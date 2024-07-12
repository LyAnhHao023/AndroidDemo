using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurninAuraItem : ItemBase
{

    CharacterInfo_1 player;

    [SerializeField]
    GameObject BurnEnemyPrefab;
    [SerializeField]
    int dmgBurn;
    [SerializeField]
    float timeToBurn;

    public override void ItemEffect()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        SetItemStat();
        player =GetComponentInParent<CharacterInfo_1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy == null)
        {
            return;
        }
        if(enemy.GetComponent<SpriteRenderer>()!=null&&enemy.GetComponentInChildren<BurnEnemy>()==null)
        {
            GameObject burnEffect= Instantiate(BurnEnemyPrefab, enemy.transform);
            burnEffect.GetComponent<BurnEnemy>().SetUp(player, timeToBurn,dmgBurn);
            
        }
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    dmgBurn = 2;
                    timeToBurn = 2;
                }
                break;
            case 2:
                {
                    dmgBurn = 4;
                    timeToBurn = 3;
                }
                break;
            case 3:
                {
                    dmgBurn = 5;
                    timeToBurn = 3;
                }
                break;
        }
    }
}
