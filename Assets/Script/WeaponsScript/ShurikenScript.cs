using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : WeaponBase
{
    [SerializeField]
    List<GameObject> ShurikenChildrenLst;
    [SerializeField]
    List<GameObject> ShurikenChildrenLst1;

    CharacterStats characterStats;

    List<GameObject> ActiveShurikenChildrenLst = new List<GameObject>();

    AudioManager audioManager;

    private void Start()
    {
        SetCharacterStats();
        BuffWeaponSizeByPersent(GetComponentInParent<CharacterInfo_1>().weaponSize);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        weaponData.maxed = false;
    }

    public override void Attack()
    {
        ActiveShurikenChildrenLst.Clear();

        foreach(GameObject child in ShurikenChildrenLst)
        {
            if (!child.activeSelf)
            {
                ActiveShurikenChildrenLst.Add(child);
                break;
            }
        }

        if(weaponStats.level >= 5)
        {
            foreach (GameObject child in ShurikenChildrenLst1)
            {
                if (!child.activeSelf)
                {
                    ActiveShurikenChildrenLst.Add(child);
                    break;
                }
            }
        }


        foreach (GameObject child in ActiveShurikenChildrenLst)
        {
            child.transform.position = transform.localPosition;
            child.SetActive(true);
        }
    }

    public void ApllyDmg(Collider2D collision)
    {
        EnemyBase enemy = collision.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            audioManager.PlaySFX(audioManager.Shuriken);

            bool isCrit = Random.value * 100 < characterStats.crit;
            float dmg = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            PostDmg((int)dmg, enemy.transform.position, isCrit);

            bool isDead = enemy.EnemyTakeDmg((int)dmg);
            if (isDead)
            {
                GetComponentInParent<CharacterInfo_1>().KilledMonster();
            }

        }
    }

    public override void SetCharacterStats()
    {
        characterStats=GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    public override void LevelUp()
    {
        weaponStats.level++;
        switch (weaponStats.level)
        {
            case 2:
                {
                    //Increase size of SHURIKEN by 20%. Increase damage of shuriken by 20%.
                    BuffWeaponSizeByPersent(0.20f);
                    BuffWeaponDamageByPersent(0.2f);
                }
                break;
            case 3:
                {
                    //Reduce delay between attacks by 20%.
                    weaponStats.timeAttack -= weaponData.stats.timeAttack * 20 / 100;
                }
                break;
            case 4:
                {
                    //Increase damage by 30%, and size by 20%.
                    BuffWeaponSizeByPersent(0.2f);
                    BuffWeaponDamageByPersent(0.3f);
                }
                break;
            case 5:
                {
                    //Increase hit limit  30% and can active two shuriken
                    foreach (var item in ShurikenChildrenLst)
                    {
                        item.GetComponent<ShurikenChildrenScript>().deactivateDistance += 3;
                    }
                    foreach (var item in ShurikenChildrenLst1)
                    {
                        item.GetComponent<ShurikenChildrenScript>().deactivateDistance += 3;
                    }
                }
                break;
            case 6:
                {
                    //Increase size aatack by 40%.
                    BuffWeaponSizeByPersent(0.4f);
                }
                break;
            case 7:
                {
                    //Increase size dmg by 50%.
                    BuffWeaponDamageByPersent(0.5f);
                    weaponData.maxed = true;
                }
                break;

            default: break;
        }
    }
}
