using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombScript : WeaponBase
{
    CharacterStats characterStats;

    [SerializeField]
    GameObject BombChildrenPrefab;

    Transform ObjectDrops;

    [SerializeField]
    int numberBombDrop=1;

    int bomDrop = 0;

    private void Start()
    {
        SetCharacterStats();
        BuffWeaponSizeByPersent(GetComponentInParent<CharacterInfo_1>().weaponSize);
        ObjectDrops = GameObject.Find("===ObjectDrop===").transform;
        weaponData.maxed = false;
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = weaponStats.timeAttack;
            bomDrop += numberBombDrop;
        }

        if(bomDrop > 0)
        {
            bomDrop--;
            Attack();
        }
    }

    public override void Attack()
    {
        GameObject newBomb = Instantiate(BombChildrenPrefab, transform.position, Quaternion.identity);
        newBomb.transform.parent = ObjectDrops;
        newBomb.transform.position = transform.position;
        newBomb.transform.localScale = transform.localScale;
    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    public override void LevelUp()
    {
        weaponStats.level++;
        switch (weaponStats.level)
        {
            case 2:
                {
                    //Increase size by 15%.
                    BuffWeaponSizeByPersent(0.15f);
                }
                break;
            case 3:
                {
                    //Increase damage by 30%.
                    BuffWeaponDamageByPersent(0.3f);
                }
                break;
            case 4:
                {
                    //Throw 2 bombs.
                    numberBombDrop = 2;
                }
                break;
            case 5:
                {
                    //Reduce the time between attacks by 30%.
                    weaponStats.timeAttack -= weaponData.stats.timeAttack * 30 / 100;
                }
                break;
            case 6:
                {
                    //Increase size by 25%. Increase damage by 30%.
                    BuffWeaponSizeByPersent(0.25f);
                    BuffWeaponDamageByPersent(0.3f);
                }
                break;
            case 7:
                {
                    //Throw 3 bombs.
                    numberBombDrop = 3;
                    weaponData.maxed = true;
                }
                break;

            default: break;
        }
    }
}
