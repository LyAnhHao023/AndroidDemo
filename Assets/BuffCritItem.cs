using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCritItem : ItemBase
{
    [SerializeField]
    float maxBuff;
    float currentBuff;

    float timer;

    int curentHp;

    CharacterStats characterStats;


    public override void SetItemStat()
    {
        switch (level)
        {
            case 2: maxBuff = 15f;
                break;
            case 3: maxBuff = 20f;
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curentHp = GetComponentInParent<CharacterInfo_1>().currentHealth;
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    // Update is called once per frame
    public override void Update()
    {
        timer-=Time.deltaTime;
        if(timer <= 0 && currentBuff < maxBuff)
        {
            timer = 1f;
            ItemEffect();
        }

        if(curentHp> GetComponentInParent<CharacterInfo_1>().currentHealth)
        {
            ResetBuff();
        }

    }
    public override void ItemEffect()
    {
        currentBuff += 1;
        characterStats.crit += 1;
    }

    private void ResetBuff()
    {
        characterStats.crit -= currentBuff;
        currentBuff = 0;
    }
}
