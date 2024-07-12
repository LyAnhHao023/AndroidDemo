using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigWeapon : ItemBase
{
    [SerializeField]
    float persentBuffBigWeapons=0.3f;

    private void Start()
    {
        level = 1;
        SetItemStat();
    }
    public override void ItemEffect()
    {
        GetComponentInParent<CharacterInfo_1>().weaponSize += persentBuffBigWeapons;

        List<weaponEnquip> weapons = GetComponentInParent<WeaponsManager>().weapons_lst;

        foreach (weaponEnquip weapon in weapons)
        {
            weapon.weaponObject.GetComponent<WeaponBase>().BuffWeaponSizeByPersent(persentBuffBigWeapons);
        }
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    ItemEffect();
                }
                break;
            case 2:
                {
                    ItemEffect();
                }
                break;
            case 3:
                {
                    ItemEffect();
                }
                break;
            case 4:
                {
                    ItemEffect();
                }
                break;
        }
    }
}
