using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExchangeAtPar : ItemBase
{
    [SerializeField]
    float persentReduceDmgTake = 0.1f;
    [SerializeField]
    float persentDmgBuff = 0.3f;

    CharacterInfo_1 player;

    float timer;

    bool isActiveBuff = false;

    public override void SetItemStat()
    {
        switch (level)
        {
            case 2:
                {
                    persentDmgBuff += 0.1f;
                    persentReduceDmgTake += 0.1f;
                    if(isActiveBuff)
                    {
                        player.attackPercent += 0.1f;
                        player.reduceDmgTake += 0.1f;
                    }
                }
            break;
                case 3:
                {
                    persentDmgBuff += 0.1f;
                    persentReduceDmgTake += 0.1f;
                    if (isActiveBuff)
                    {
                        player.attackPercent += 0.1f;
                        player.reduceDmgTake += 0.1f;
                    }
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<CharacterInfo_1>();
        ItemEffect();
    }

    // Update is called once per frame
    public override void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0) 
        {
            if(player.coins>0)
            {
                timer = 1f;
                player.coins -= 1;
                if (!isActiveBuff)
                {
                    ItemEffect();
                }
            }
            else if(isActiveBuff)
            {
                DeActiveBuff();
            }
        }
    }

    public override void ItemEffect()
    {
        player.attackPercent += persentDmgBuff;
        player.reduceDmgTake += persentReduceDmgTake;
        isActiveBuff = true;
    }

    private void DeActiveBuff()
    {
        player.attackPercent -= persentDmgBuff;
        player.reduceDmgTake -= persentReduceDmgTake;
        isActiveBuff = false;
    }
}
