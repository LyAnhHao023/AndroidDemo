using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ToxinZonesScript : WeaponBase
{
    CharacterStats characterStats;
    List<EnemyBase> slowedEnemies = new List<EnemyBase>();

    [SerializeField]
    int speedSlow=1;

    bool isKnockBack=false;

    AudioManager audioManager;

    private void Start()
    {
        SetCharacterStats();
        BuffWeaponSizeByPersent(GetComponentInParent<CharacterInfo_1>().weaponSize);
        ////SetRadiusEffect(transform.localScale.x);
        //currentSizeWeapon=transform.localScale.x;

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        weaponData.maxed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemy= collision.gameObject.GetComponent<EnemyBase>();
        if (enemy!=null&& !slowedEnemies.Contains(enemy))
        {
            enemy.GetComponent<EnemyBase>().IncreaseDecreaseSpeed(-speedSlow);
            slowedEnemies.Add(enemy);
        }
    }

    private void ApplyDmg(EnemyBase enemy)
    {
        bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;
        float dmg = isCrit ?
            (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

        PostDmg((int)dmg, enemy.transform.position, isCrit);

        bool isDead = enemy.EnemyTakeDmg((int)dmg);
        if (isDead)
        {
            GetComponentInParent<CharacterInfo_1>().KilledMonster();
        }
        //KnockBack
        if (!enemy.GetComponent<Collider2D>().isTrigger&&isKnockBack)
        {
            Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
            enemy.Knockback(knockbackDirection, 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
        if (enemy!=null&& slowedEnemies.Contains(enemy))
        {
            enemy.GetComponent<EnemyBase>().IncreaseDecreaseSpeed(speedSlow);
            slowedEnemies.Remove(enemy);
        }
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0&&slowedEnemies.Count>0)
        {
            timer = weaponStats.timeAttack;
            List<EnemyBase> tempEnemies = new List<EnemyBase>(slowedEnemies);

            audioManager.PlaySFX(audioManager.ToxinZone);

            foreach (var item in tempEnemies)
            {
                if (item != null)
                {
                    ApplyDmg(item);
                }
            }
        }

        //if (transform.localScale.x != currentSizeWeapon)
        //{
        //    currentSizeWeapon = transform.localScale.x;
        //    SetRadiusEffect(currentSizeWeapon);
        //}
    }

    //private void SetRadiusEffect(float x)
    //{
    //    ParticleSystem ps = GetComponent<ParticleSystem>();
    //    ParticleSystem.ShapeModule shape = ps.shape;
    //    shape.radius = x*3.5f;//3 la radius mac dinh cua effect
    //}

    public override void Attack()
    {
        
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
                    //Increase area by 15%.
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
                    //Increase area by 25%.
                    BuffWeaponSizeByPersent(0.25f);
                }
                break;
            case 5:
                {
                    //Increase frequency of hits by 20%.
                    weaponStats.timeAttack -= weaponData.stats.timeAttack * 30 / 100;
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
                    //Add small knockback on hit.
                    isKnockBack = true;
                    weaponData.maxed = true;
                }
                break;

            default: break;
        }
    }
}
