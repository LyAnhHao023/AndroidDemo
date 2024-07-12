using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMagicWeapon : WeaponBase
{
    [SerializeField]
    List<GameObject> tornadoList = new List<GameObject>();

    int countLst=0;
    int numActiveTornado = 2;
    int countTornado=0;

    CharacterStats characterStats;

    public override void Attack()
    {
        if (countTornado > 0)
        {
            countTornado --;
            tornadoList[countLst].SetActive(true);

            countLst = countLst + 1 >= tornadoList.Count ? 0 : ++countLst;
        }
    }

    public override void LevelUp()
    {
        weaponStats.level++;
        switch (weaponStats.level)
        {
            case 2:
                {
                    //Increase area by 40%.
                    BuffWeaponSizeByPersent(0.4f);

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
                    //Increase area by 40%.
                    BuffWeaponSizeByPersent(0.4f);
                    //Increase frequency of hits by 30%.
                    weaponStats.timeAttack -= weaponData.stats.timeAttack * 30 / 100;
                }
                break;
            case 5:
                {
                    //can active 2 axe at sametime, Increase damage by 30%.
                    BuffWeaponDamageByPersent(0.2f);
                    numActiveTornado = 3;
                }
                break;
            case 6:
                {
                    //Increase damage by 60%.
                    BuffWeaponDamageByPersent(0.6f);
                }
                break;
            case 7:
                {
                    //Increase damage by 50%.
                    BuffWeaponDamageByPersent(0.5f);
                    weaponData.maxed = true;
                }
                break;

            default: break;
        }
    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }



    // Start is called before the first frame update
    void Start()
    {
        SetCharacterStats();
        CharacterInfo_1 characterInfo_1 = GetComponentInParent<CharacterInfo_1>();
        BuffWeaponSizeByPersent(characterInfo_1.weaponSize);

        foreach (var item in tornadoList)
        {
            item.GetComponent<TornadoOfWindMagic>().SetData(characterStats, weaponStats);
        }

    }

    // Update is called once per frame
    public override void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            timer = weaponStats.timeAttack;
            countTornado+=numActiveTornado;
        }
        Attack();
    }
}
