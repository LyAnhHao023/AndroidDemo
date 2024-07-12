using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    int dmgBullet=2;
    [SerializeField]
    float timeAutoDestroy=10f;
    Animator animator;
    bool isCrit;
    private Vector3 baseSizeBullet;

    //xuyen thau
    public bool isPenetrating=false;

    public void SetDmg(int dmg, bool isCrit)
    {
        dmgBullet= dmg;
        this.isCrit = isCrit;
        animator = GetComponent<Animator>();
    }

    public void BuffSizeBulletByPersent(float persent)
    {
        transform.localScale += new Vector3(baseSizeBullet.x * persent, baseSizeBullet.y * persent, baseSizeBullet.y * persent);
    }

    private void Awake()
    {
        Destroy(gameObject, timeAutoDestroy);
        baseSizeBullet = transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        EnemyBase enemy= collision.gameObject.GetComponent<EnemyBase>();
        if(enemy!=null)
        {
            if (isPenetrating)
            {
                MessengerSystem.instance.DmgPopUp(dmgBullet.ToString(), enemy.transform.position, isCrit);
                bool isDead = enemy.EnemyTakeDmg(dmgBullet);
                if (isDead)
                {
                    GameObject.Find("Player").GetComponent<CharacterInfo_1>().KilledMonster();
                }
                dmgBullet -=(int)Mathf.Ceil( dmgBullet * 20 /100);
                if (dmgBullet == 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                animator.SetBool("isExplode", true);
                MessengerSystem.instance.DmgPopUp(dmgBullet.ToString(), enemy.transform.position, isCrit);
                bool isDead = enemy.EnemyTakeDmg(dmgBullet);
                if (isDead)
                {
                    GameObject.Find("Player").GetComponent<CharacterInfo_1>().KilledMonster();
                }
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = Vector3.zero;
                Destroy(gameObject, 0.4f);
            }

        }
    }
}
