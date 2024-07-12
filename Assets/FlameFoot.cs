using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFoot : ItemBase
{
    CharacterStats characterStats;
    playerMove playerMove;

    [SerializeField]
    GameObject flamePrefab;

    Transform dropPos;

    [SerializeField]
    float timeCreateFlame=0;
    float timer;
    public override void ItemEffect()
    {
        GameObject flame=Instantiate(flamePrefab, dropPos);
        flame.transform.position=transform.position;
        flame.GetComponent<Flame>().SetDmg(characterStats.strenght / 3);

    }

    public override void SetItemStat()
    {
        return;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterStats=GetComponentInParent<CharacterInfo_1>().characterStats;
        playerMove = GetComponentInParent<playerMove>();
        dropPos = GameObject.Find("BulletsObject").transform;
    }

    // Update is called once per frame
    public override void Update()
    {
        timer-= Time.deltaTime;
        if(timer <= 0&& playerMove.moveInput.sqrMagnitude>0)
        {
            timer = timeCreateFlame;
            ItemEffect();
        }
    }
}
