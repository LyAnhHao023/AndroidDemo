using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShieldItem : ItemBase
{
    CharacterInfo_1 player;
    [SerializeField]
    int shieldPlus = 30;
    [SerializeField]
    float timeRecoveryShield = 25;
    float timer;
    Animator animator;

    private void Start()
    {
        level = 1;
        SetItemStat();
        animator = GetComponent<Animator>();
        player = GetComponentInParent<CharacterInfo_1>();
        player.PlusMaxSheild(shieldPlus);
        timer = timeRecoveryShield;
    }

    private void SetAnimationOff()
    {
        animator.SetBool("Broken", false);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public override void Update()
    {

        if (player.shieldCurrentValue <= 0&& animator.enabled==true)
        {
            animator.SetBool("Broken", true);
            Invoke("SetAnimationOff",0.4f);
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        timer -= Time.deltaTime;
        if(timer < 0)
        {
            timer = timeRecoveryShield;
            ItemEffect();
        }
    }

    public override void ItemEffect()
    {
        player.ShieldRecovery(shieldPlus);
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    shieldPlus = 30;
                    timeRecoveryShield = 25;
                }
                break;
            case 2:
                {
                    shieldPlus = 40;
                    timeRecoveryShield = 20;
                }
                break;
            case 3:
                {
                    shieldPlus = 50;
                    timeRecoveryShield = 15;
                }
                break;
            case 4:
                {
                    shieldPlus = 75;
                    timeRecoveryShield = 15;
                }
                break;
        }
    }
}
