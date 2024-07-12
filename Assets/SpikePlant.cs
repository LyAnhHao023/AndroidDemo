using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikePlant : MonoBehaviour
{
    CharacterStats characterStats;
    WeaponStats weaponStats;

    List<int> IdEnemy=new List<int>();

    [SerializeField]
    float timerApplyDmg=0.5f;
    [SerializeField]
    float timeDeActive = 8f;

    float timer;

    CharacterInfo_1 player;

    public void SetData(CharacterStats characterStats, WeaponStats weaponStats)
    {
        this.characterStats = characterStats;
        this.weaponStats = weaponStats;
        StartCoroutine(DeActive());
        transform.parent = GameObject.FindGameObjectWithTag("DropOJ").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInfo_1>();
        transform.localScale += new Vector3(transform.localScale.x * player.weaponSize, transform.localScale.y * player.weaponSize, 0);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <=0)
        {
            timer = timerApplyDmg;
            IdEnemy.Clear();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBase enemy=collision.GetComponent<EnemyBase>();
        if (enemy != null&& !IdEnemy.Contains(collision.GetInstanceID()))
        {
            IdEnemy.Add(collision.GetInstanceID());
            bool isCrit = Random.value * 100 < characterStats.crit;
            float dmg = isCrit ?
                (weaponStats.dmg + characterStats.strenght) * characterStats.critDmg : (weaponStats.dmg + characterStats.strenght);

            PostDmg((int)dmg, enemy.transform.position, isCrit);

            bool isDead = enemy.EnemyTakeDmg((int)(dmg/2));
            if (isDead)
            {
                player.KilledMonster();
            }
        }
        
    }

    public IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActive);
        gameObject.SetActive(false);
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }

}
