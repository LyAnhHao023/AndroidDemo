using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassOfKnowledgeItem : ItemBase
{
    [SerializeField]
    float expPersent=0.2f;
    CharacterInfo_1 player;

    public override void ItemEffect()
    {
        player.expPercent += expPersent;
    }

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        SetItemStat();
        player =GetComponentInParent<CharacterInfo_1>();
        ItemEffect();
    }

    public override void SetItemStat()
    {
        switch (level)
        {
            case 1:
                {
                    expPersent = 0.2f;
                }
                break;
            case 2:
                {
                    player.expPercent += 0.2f;
                }
                break;
            case 3:
                {
                    player.expPercent += 0.3f;
                }
                break;
            case 4:
                {
                    player.expPercent += 0.3f;
                }
                break;
            case 5:
                {
                    player.expPercent += 0.5f;
                }
                break;
            case 6:
                {
                    player.expPercent += 0.5f;
                }
                break;
        }
    }
}
