using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IncreaseHitSpeed : ItemBase
{
    [SerializeField]
    int persentSpeedIncrease;
    [SerializeField]
    int totalPersentSpeedIncrease;
    List<weaponEnquip> weaponEnquips_lst;

    bool isJustEnquip=true;

    private void Start()
    {
        level = 1;
        SetItemStat();
    }

    public override void ItemEffect()
    {
        // get list weapon enquiped and buff attack speed
        weaponEnquips_lst = GetComponentInParent<WeaponsManager>().weapons_lst;
        foreach (var item in weaponEnquips_lst)
        {
            
            float curentTimeAttack = item.weaponObject.GetComponent<WeaponBase>().weaponStats.timeAttack;
            Debug.Log(curentTimeAttack+"truoc khi tang");
            item.weaponObject.GetComponent<WeaponBase>().weaponStats.timeAttack -= curentTimeAttack * persentSpeedIncrease/100;
            Debug.Log(item.weaponObject.GetComponent<WeaponBase>().weaponStats.timeAttack+"Sau khi tang");
        }
        isJustEnquip=false;
    }

    public override void Update()
    {
        if (!isJustEnquip)
        {
            if (weaponEnquips_lst.Count < GetComponentInParent<WeaponsManager>().weapons_lst.Count)
            {
                float curentTimeAttack = weaponEnquips_lst.Last().weaponObject.GetComponent<WeaponBase>().weaponStats.timeAttack;
                weaponEnquips_lst.Last().weaponObject.GetComponent<WeaponBase>().weaponStats.timeAttack -= curentTimeAttack * totalPersentSpeedIncrease / 100;
            }
        }
        
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    persentSpeedIncrease = 10;
                    totalPersentSpeedIncrease = persentSpeedIncrease; // totalPersentSpeedIncrease = 10
                }
                break;
            case 2:
                {
                    totalPersentSpeedIncrease += persentSpeedIncrease; // totalPersentSpeedIncrease = 20
                }
                break;
            case 3:
                {
                    totalPersentSpeedIncrease += persentSpeedIncrease; // totalPersentSpeedIncrease = 30
                }
                break;
            case 4:
                {
                    persentSpeedIncrease = 20;
                    totalPersentSpeedIncrease += persentSpeedIncrease; // totalPersentSpeedIncrease = 50
                }
                break;
            case 5:
                {
                    persentSpeedIncrease = 25;
                    totalPersentSpeedIncrease += persentSpeedIncrease; // totalPersentSpeedIncrease = 75
                }
                break;
        }

        ItemEffect();
    }
}
