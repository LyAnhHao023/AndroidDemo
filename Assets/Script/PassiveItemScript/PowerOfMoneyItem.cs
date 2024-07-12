using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOfMoneyItem : ItemBase
{
    [SerializeField]
    int persentDropCoinBuffInt;
    [SerializeField]
    float persentAttackBuffFloat;

    CharacterInfo_1 player;
    int coinCurrent;
    private void Start()
    {
        level = 1;
        SetItemStat();
        player = GetComponentInParent<CharacterInfo_1>();
        coinCurrent = player.coins;
        GameObject.Find("===SpawEnemy===").GetComponent<SpawEnemy>().PlusOrMinusEnemyStats(0,0,0,0, persentDropCoinBuffInt, 0,0);
    }

    private void FixedUpdate()
    {
        SetItemStat();
        ItemEffect();
    }


    public override void ItemEffect()
    {
        if (coinCurrent < player.coins)
        {
            int persentBuff = player.coins - coinCurrent;
            player.attackPercent += persentBuff * persentAttackBuffFloat;
            //player.statUpdate();
            coinCurrent = player.coins;
        }
    }

    public override void SetItemStat()
    {
        switch(level)
        {
            case 1:
                {
                    persentDropCoinBuffInt = 5;
                }
                break;
            case 2:
                {
                    persentDropCoinBuffInt = 10;
                }
                break;
            case 3:
                {
                    persentDropCoinBuffInt = 20;
                }
                break;
            case 4:
                {
                    persentDropCoinBuffInt = 30;
                }
                break;
        }
    }
}
