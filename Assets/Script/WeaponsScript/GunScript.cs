using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : WeaponBase
{
    [SerializeField]
    GameObject Bullet;

    [SerializeField]
    Transform firePos;

    [SerializeField]
    float bulletForce=1;

    [SerializeField]
    GameObject imgFire;
    [SerializeField]
    GameObject fireEffect;

    CharacterStats characterStats;

    [SerializeField]
    Transform bulletsObject;

    CharacterInfo_1 characterInfo_1;

    //dung cho viec nang cap
    private float buffSizeBullet = 0;

    bool isPenetrating=false;

    bool isFire3Bullet=false;

    AudioManager audioManager;

    private void Start()
    {
        SetCharacterStats();
        bulletsObject = GameObject.Find("BulletsObject").transform;
        characterInfo_1=GetComponentInParent<CharacterInfo_1>();
        BuffWeaponSizeByPersent(characterInfo_1.weaponSize);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        weaponData.maxed = false;
    }

    public override void Update()
    {
        if(Time.deltaTime != 0)
            RotationGun();
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = weaponStats.timeAttack;
            if (isFire3Bullet)
            {
                StartCoroutine(Fire3Bullet());
            }
            else
            {
                Attack();
            }
        }
    }

    private void RotationGun()
    {
        Vector3 mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir=mousePos-transform.position;
        float angle=Mathf.Atan2(lookDir.y,lookDir.x)*Mathf.Rad2Deg;

        Quaternion rotation=Quaternion.Euler(0,0, angle);
        transform.rotation = rotation;
        if(transform.eulerAngles.z>90&& transform.eulerAngles.z < 270)
        {
            transform.localScale=new Vector3(1, -1,1);
            fireEffect.transform.rotation = Quaternion.Euler( 0, 180, 0);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
            fireEffect.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private IEnumerator Fire3Bullet()
    {
        Attack();
        yield return new WaitForSeconds(0.05f);
        Attack();
        yield return new WaitForSeconds(0.05f);
        Attack();
    }

    public override void Attack()
    {
        //Audio
        audioManager.PlaySFX(audioManager.Gun);

        GameObject createBullet = Instantiate(Bullet, firePos.position, Quaternion.identity);

        BulletScript bulletScript = createBullet.GetComponent<BulletScript>();

        bulletScript.isPenetrating = isPenetrating;

        bulletScript.BuffSizeBulletByPersent(buffSizeBullet+characterInfo_1.weaponSize);

        createBullet.transform.parent = bulletsObject.transform;
        //Set dmg
        bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;

        float dmg = isCrit ?
                    (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);
        bulletScript.SetDmg((int)dmg,isCrit);
        Rigidbody2D rigidbody2D = createBullet.GetComponent<Rigidbody2D>();
        rigidbody2D.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

        //effect gun fire
        imgFire.SetActive(true);
        fireEffect.SetActive(true);
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
                    //Increase size by 3%.
                    BuffWeaponSizeByPersent(0.3f);
                    buffSizeBullet += 0.3f;
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
                    //Reduce the time between attacks by 30%.
                    weaponStats.timeAttack -= weaponData.stats.timeAttack * 30 / 100;

                }
                break;
            case 5:
                {
                    //can fire 3 bullet at sametime
                    isFire3Bullet = true;
                    
                }
                break;
            case 6:
                {
                    //Increase size by 30%. Increase damage by 30%.
                    BuffWeaponSizeByPersent(0.3f);
                    buffSizeBullet += 0.3f;
                    BuffWeaponDamageByPersent(0.3f);
                }
                break;
            case 7:
                {
                    //Throw 3 bombs.
                    isPenetrating = true;
                    weaponData.maxed = true;
                }
                break;

            default: break;
        }
    }
}
