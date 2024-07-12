using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowHealthItem : ItemBase
{
    [SerializeField] float timeDe;
    [SerializeField] int healthDe;
    CharacterInfo_1 player;

    float time;

    private void Start()
    {
        level = 1;
        SetItemStat();
        player = GetComponentInParent<CharacterInfo_1>();
    }

    public override void ItemEffect()
    {
        player.SlowHealthDe(healthDe);
    }

    public override void Update()
    {
        time += Time.deltaTime;

        if (time >= timeDe)
        {
            ItemEffect();
            time = 0;
        }
    }

    public override void SetItemStat()
    {
        switch(level)
        {
            case 1:
                {
                    healthDe = 3;
                    timeDe = 0.2f;
                }
                break;
            case 2:
                {
                    healthDe = 2;
                    timeDe = 0.3f;
                }
                break;
            case 3:
                {
                    healthDe = 2;
                    timeDe = 0.4f;
                }
                break;
            case 4:
                {
                    healthDe = 1;
                    timeDe = 0.4f;
                }
                break;
        }
    }
}
