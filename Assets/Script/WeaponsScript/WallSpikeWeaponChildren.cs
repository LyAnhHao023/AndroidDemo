using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallSpikeWeaponChildren : MonoBehaviour
{
    CharacterStats characterStats;

    WeaponStats weaponStats;

    Vector3 positonAttack;

    [SerializeField]
    float timeAttack = 1f;

    float timer = 0;

    bool attack = true;

    float timeDeActice = 2f;

    List<int> idEnemy= new List<int>();

    public void SetData(CharacterStats characterStats, WeaponStats weaponStats)
    {
        this.characterStats = characterStats;
        this.weaponStats = weaponStats;
    }

    public void SetTimeDeActive(float timeDeActice)
    {
        this.timeDeActice = timeDeActice;
    }

    public void SetPositionAttack(Vector3 positonAttack, Vector3 tranformBase, Quaternion rotation)
    {
        this.positonAttack = positonAttack;
        transform.position = tranformBase;
        transform.rotation = rotation;
        StartCoroutine(DeActive());
    }



    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = timeAttack;
            idEnemy.Clear();
            attack = true;
            StartCoroutine(DeAttack());
        }



        Vector3 dic=positonAttack- transform.position;

        float distance = Vector2.Distance(transform.position + dic * 10 * Time.deltaTime, positonAttack);

        if (distance <= 0.5)
        {
            transform.position = positonAttack;
        }

        transform.position += dic * 10 * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            positonAttack=transform.position;
        }

        EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();

        if (enemy != null && attack && !idEnemy.Contains(enemy.GetInstanceID()))
        {
            idEnemy.Add(enemy.GetInstanceID());

            bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;
            float dmgApply = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            MessengerSystem.instance.DmgPopUp(((int)dmgApply).ToString(), enemy.transform.position, isCrit);

            bool isDead = enemy.EnemyTakeDmg((int)dmgApply);
            if (isDead)
            {
                GetComponentInParent<CharacterInfo_1>().KilledMonster();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            positonAttack = transform.position;
        }

        EnemyBase enemy=collision.gameObject.GetComponent<EnemyBase>();

        if (enemy != null&& attack&& !idEnemy.Contains(enemy.GetInstanceID())) 
        {
            idEnemy.Add(enemy.GetInstanceID());

            bool isCrit = UnityEngine.Random.value * 100 < characterStats.crit;
            float dmgApply = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            MessengerSystem.instance.DmgPopUp(((int)dmgApply).ToString(), enemy.transform.position, isCrit);

            bool isDead = enemy.EnemyTakeDmg((int)dmgApply);
            if (isDead)
            {
                GetComponentInParent<CharacterInfo_1>().KilledMonster();
            }
        }
    }

    public IEnumerator DeAttack()
    {
        yield return new WaitForSeconds(0.3f);
        attack = false;
    }

    public IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActice);
        gameObject.SetActive( false);
    }
}
