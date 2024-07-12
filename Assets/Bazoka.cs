using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bazoka : WeaponBase
{
    [SerializeField]
    GameObject BulletBazoka;

    [SerializeField]
    Transform firePos;
    [SerializeField]
    GameObject fireEffect;


    float bulletForce = 20;

    CharacterStats characterStats;

    [SerializeField]
    Transform bulletsObject;

    SpriteRenderer spriteRenderer;

    CharacterInfo_1 characterInfo_1;

    private float buffSizeBullet=0;

    private void Start()
    {
        SetCharacterStats();
        bulletsObject = GameObject.Find("BulletsObject").transform;
        spriteRenderer=GetComponent<SpriteRenderer>();
        characterInfo_1=GetComponentInParent<CharacterInfo_1>();
        BuffWeaponSizeByPersent(characterInfo_1.weaponSize);
    }

    public override void Update()
    {
        bool isRotation = false;

        if (Time.deltaTime != 0)
        {
            isRotation = RotationGun();
        }
        timer -= Time.deltaTime;

        if (timer <= 0 && isRotation)
        {
            timer = weaponStats.timeAttack;
            Attack();
        }
    }

    private EnemyBase FindEnemy()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 20f);
        float minDis = 100f;
        EnemyBase enemyNear = null;
        foreach (Collider2D item in enemies)
        {
            EnemyBase enemy = item.GetComponent<EnemyBase>();
            if (enemy != null)
            {
                float dis = Vector2.Distance(transform.position, enemy.transform.position);
                if (minDis > dis)
                {
                    minDis = dis;
                    enemyNear = enemy;
                }
            }
        }
        return enemyNear;
    }

    private bool RotationGun()
    {
        EnemyBase enemy = FindEnemy();
        if (enemy == null)
        {
            return false;
        }
        Vector2 lookDir = enemy.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        transform.rotation = rotation;
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
        {
            spriteRenderer.flipY = true;
            firePos.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            spriteRenderer.flipY = false;
            firePos.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        return true;
    }

    public override void Attack()
    {

        GameObject createBullet = Instantiate(BulletBazoka, firePos.position, transform.rotation);
        BazokaBullet bulletScript = createBullet.GetComponent<BazokaBullet>();

        bulletScript.BuffSizeBulletByPersent(buffSizeBullet + characterInfo_1.weaponSize);
        createBullet.transform.parent = bulletsObject.transform;
        //Set dmg
        bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;

        float dmg = isCrit ?
                    (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);
        bulletScript.SetDmg((int)dmg, isCrit);
        Rigidbody2D rigidbody2D = createBullet.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        //effect gun fire

        fireEffect.SetActive(true);
    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    public override void LevelUp()
    {
        throw new System.NotImplementedException();
    }
}
