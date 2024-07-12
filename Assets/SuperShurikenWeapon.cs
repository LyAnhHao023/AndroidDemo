using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperShurikenWeapon : WeaponBase
{
    [SerializeField]
    List<GameObject> shurikenList = new List<GameObject>();

    CharacterStats characterStats;

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void LevelUp()
    {
        throw new System.NotImplementedException();
    }

    public override void SetCharacterStats()
    {
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetCharacterStats();
        CharacterInfo_1 characterInfo_1 = GetComponentInParent<CharacterInfo_1>();
        BuffWeaponSizeByPersent(characterInfo_1.weaponSize);

        foreach (var item in shurikenList)
        {
            item.GetComponent<SuperShurikenChildren>().SetData(characterStats, weaponStats);
        }
    }

    public override void Update() { return; }
}
