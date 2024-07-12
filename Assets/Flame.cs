using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    int dmg;
    [SerializeField]
    float timeApplyDmg = 1f;
    [SerializeField]
    float timeAutoDestroy = 5f;
    float timer = 0f;

    CharacterInfo_1 player;

    List<int> IdEnemy=new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindWithTag("Player").GetComponent<CharacterInfo_1>();
        Destroy(gameObject, timeAutoDestroy);
    }

    public void SetDmg(int dmg)
    {
        this.dmg=dmg;
    }

    // Update is called once per frame
    void Update()
    {
        timer-=Time.deltaTime;
        if(timer <= 0f)
        {
            timer = timeApplyDmg;
            IdEnemy.Clear();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        EnemyBase enemy=collision.GetComponent<EnemyBase>();
        if (enemy != null&& !IdEnemy.Contains(collision.GetInstanceID()))
        {
            IdEnemy.Add(collision.GetInstanceID());
            bool isDead = enemy.EnemyTakeDmg(dmg);
            PostDmg(dmg, collision.transform.position, false);
            if (isDead)
            {
                player.KilledMonster();
            }
        }
    }

    private void PostDmg(int dmg, Vector3 TargetPosition, bool isCrit)
    {
        MessengerSystem.instance.DmgPopUp(dmg.ToString(), TargetPosition, isCrit);
    }
}
