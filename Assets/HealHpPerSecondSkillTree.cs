using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHpPerSecondSkillTree : ItemBase
{
    [SerializeField]
    int persentRegenHp = 1;
    [SerializeField]
    float timeRegen = 2;

    float timer;

    CharacterInfo_1 player;
    public override void ItemEffect()
    {
        player.HealthByPercent(persentRegenHp);
    }

    public override void SetItemStat()
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<CharacterInfo_1>();
    }

    // Update is called once per frame
    public override void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 0)
        {
            timer = timeRegen;
            ItemEffect();   
        }
    }
}
