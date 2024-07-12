using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSucking : ItemBase
{
    [SerializeField]
    int health;

    int monsterKilled=0;

    private void Start()
    {
        level = 1;
        SetItemStat();
    }

    public override void ItemEffect()
    {

        int updateMonsterKilled =  GetComponentInParent<CharacterInfo_1>().numberMonsterKilled;
        if (monsterKilled < updateMonsterKilled)
        {
            monsterKilled = updateMonsterKilled;
            GetComponentInParent<CharacterInfo_1>().HealthByNumber(health);
        }
    }

    public override void Update()
    {
        ItemEffect();
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    health = 1;
                }
                break;
            case 2:
                {
                    health = 2;
                }
                break;
            case 3:
                {
                    health = 3;
                }
                break;
            case 4:
                {
                    health = 5;
                }
                break;
        }
    }
}
