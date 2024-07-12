using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    public WeaponData weaponData;

    public WeaponStats weaponStats;

    public float timer=0;

    public WeaponStats baseStats;

    private Vector3 baseSizeWeapon;


    private void Awake()
    {
        baseSizeWeapon = transform.localScale;
    }

    public virtual void Update()
    {
        timer-=Time.deltaTime;
        if (timer < 0)
        {
            timer = weaponStats.timeAttack;
            Attack();
        }
    }

    public virtual void SetData(WeaponData wd)
    {
        weaponData = wd;
        weaponStats.SetStats(wd.stats);
        baseStats.SetStats(wd.stats);
    }

    public void BuffWeaponSizeByPersent(float persent)
    {
        transform.localScale+=new Vector3(baseSizeWeapon.x * persent, baseSizeWeapon.y * persent, 0);
    }

    public void BuffWeaponDamageByPersent(float persent)
    {
        int damage =(int)Mathf.Ceil((float)baseStats.dmg*persent);
        weaponStats.dmg += damage;
    }

    public void OverLevelUp()
    {
        weaponStats.dmg += 1;
    }

    public abstract void LevelUp();

    public abstract void Attack();

    public abstract void SetCharacterStats();

    public virtual void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }
}
