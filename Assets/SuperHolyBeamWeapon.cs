using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHolyBeamWeapon : WeaponBase
{

    [SerializeField] GameObject rightBeam;
    [SerializeField] GameObject leftBeam;

    CharacterStats characterStats;

    int knockBackMass = 6;

    AudioManager audioManager;

    float rotationSpeed=200f;

    private void Start()
    {
        SetCharacterStats();
        BuffWeaponSizeByPersent(GetComponentInParent<CharacterInfo_1>().weaponSize);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ApllyDmg(Collider2D collision)
    {
        EnemyBase z = collision.GetComponent<EnemyBase>();
        if (z != null)
        {
            bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;
            float dmg = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            PostDmg((int)dmg, z.transform.position, isCrit);

            bool isDead = z.EnemyTakeDmg((int)dmg);
            if (isDead)
            {
                GetComponentInParent<CharacterInfo_1>().KilledMonster();
            }
            //KnockBack
            if (!z.GetComponent<Collider2D>().isTrigger)
            {
                Vector2 knockbackDirection = (z.transform.position - transform.position).normalized;
                z.Knockback(knockbackDirection, knockBackMass);
            }
        }
    }


    public override void Attack()
    {
        ActiveTwoBeam();
    }

    public override void Update()
    {
        base.Update();
        //xoay
        float angle = transform.eulerAngles.z + rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    private void ActiveTwoBeam()
    {
        audioManager.PlaySFX(audioManager.HolyBeam);
        audioManager.PlaySFX(audioManager.HolyBeam);
        rightBeam.SetActive(true);
        leftBeam.SetActive(true);

    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    public override void LevelUp()
    {
        return;
    }
}
