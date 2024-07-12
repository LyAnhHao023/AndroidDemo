using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceCoolDownSkill : ItemBase
{
    CharacterInfo_1 player;

    [SerializeField]
    float persentReduce = 0.2f;

    float cdBase;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<CharacterInfo_1>();
        //save time base cd
        cdBase = player.skillInfor.cdSkill;
        ItemEffect();
    }

    public override void ItemEffect()
    {
        player.skillInfor.cdSkill -= cdBase * persentReduce;
    }

    public override void SetItemStat()
    {
        switch(level)
        {
            case 2:
                {
                    //reduce 25%
                    persentReduce = 0.05f;
                    ItemEffect();
                    break;
                }
            case 3:
                {
                    //reduce 30%
                    persentReduce = 0.05f;
                    ItemEffect();
                    break;
                }
            case 4:
                {
                    //reduce 35%
                    persentReduce = 0.05f;
                    ItemEffect();
                    break;
                }
            case 5:
                {
                    //reduce 40%
                    persentReduce = 0.05f;
                    ItemEffect();
                    break;
                }

            default: break;
        }
    }
}
