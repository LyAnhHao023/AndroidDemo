using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkilTreeManager : MonoBehaviour
{
    [SerializeField]
    UpgradeData BigWeaponItemPrefab;
    [SerializeField]
    UpgradeData ReduceCdSkillPrefab;
    [SerializeField ]
    UpgradeData BuffCritPrefab;

    [SerializeField]
    ItemsData HealHpPerSeconds;
    [SerializeField]
    ItemsData FlameFoot;

    [SerializeField] CharacterInfo_1 characterInfo;

    float healthPercent = 0;
    float attackPercent = 0;
    float speedPercent = 0;
    float critPercent = 0;
    float critDmgPercent = 0;
    float reduceDmgTake = 0;
    float reduceCdSkill = 0;
    float speed=0;
    float evasion=0;


    public void LoadBuffSkillTree(SkillTree skillTree)
    {

        if (skillTree.type == 0) return;
        //chiến binh
        else if (skillTree.type == 1)
        {
            switch (skillTree.level)
            {
                case 1:
                    {
                        healthPercent += 0.02f;
                        break;
                    }
                case 2:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        break;
                    }
                case 3:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        break;
                    }
                case 4:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        break;
                    }
                case 5:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        break;
                    }
                case 6:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        characterInfo.AddItem(BigWeaponItemPrefab);
                        break;
                    }
                case 7:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        characterInfo.AddItem(BigWeaponItemPrefab);
                        healthPercent += 0.05f;
                        break;
                    }
                case 8:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        characterInfo.AddItem(BigWeaponItemPrefab);
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        break;
                    }
                case 9:
                    {
                        healthPercent += 0.02f;
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.02f;
                        attackPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        characterInfo.AddItem(BigWeaponItemPrefab);
                        healthPercent += 0.05f;
                        reduceDmgTake += 0.03f;
                        attackPercent += 0.05f;
                        break;
                    }
                case 10:
                    {
                        //buff 2% hp
                        healthPercent += 0.02f;
                        //buff 5% hp
                        healthPercent += 0.05f;
                        //reduce dmg take 2%
                        reduceDmgTake += 0.02f;
                        // buff atk 5%
                        attackPercent += 0.05f;
                        //reduce dmg take 3%
                        reduceDmgTake += 0.03f;
                        //add BigWeaponItem at begin
                        characterInfo.AddItem(BigWeaponItemPrefab);
                        //buff 5% hp
                        healthPercent += 0.05f;
                        // readuce dmg take 3%
                        reduceDmgTake += 0.03f;
                        //buff atk 5%
                        attackPercent += 0.05f;
                        //add HealHpPerSeconds effect skill tree at begin 
                        GetComponent<PassiveItemsManager>().AddItem(HealHpPerSeconds);
                        break;
                    }
            }
        }
        //Pháp sư
        else if (skillTree.type == 2)
        {
            switch (skillTree.level)
            {
                case 1:
                    {
                        reduceCdSkill += 0.02f;
                        break;
                    }
                case 2:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        break;
                    }
                case 3:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        break;
                    }
                case 4:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        break;
                    }
                case 5:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        break;
                    }
                case 6:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        characterInfo.AddItem(ReduceCdSkillPrefab);
                        break;
                    }
                case 7:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        characterInfo.AddItem(ReduceCdSkillPrefab);
                        reduceCdSkill += 0.05f;
                        break;
                    }
                case 8:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        characterInfo.AddItem(ReduceCdSkillPrefab);
                        reduceCdSkill += 0.05f;
                        speed += 0.15f;
                        break;
                    }
                case 9:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        characterInfo.AddItem(ReduceCdSkillPrefab);
                        reduceCdSkill += 0.05f;
                        speed += 0.15f;
                        reduceCdSkill += 0.05f;
                        break;
                    }
                case 10:
                    {
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.02f;
                        reduceCdSkill += 0.02f;
                        attackPercent += 0.03f;
                        speed += 0.1f;
                        characterInfo.AddItem(ReduceCdSkillPrefab);
                        reduceCdSkill += 0.05f;
                        speed += 0.15f;
                        reduceCdSkill += 0.05f;
                        // taọ ra vệt lửa để lại phía sau khi người chơi di chuyển vệt lửa gây sát thương cho kẻ địch
                        GetComponent<PassiveItemsManager>().AddItem(FlameFoot);
                        break;
                    }
            }
        }
        //sát thủ
        else if (skillTree.type == 3)
        {
            switch (skillTree.level)
            {
                case 1:
                    {
                        critPercent += 2f;
                        break;
                    }
                case 2:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        break;
                    }
                case 3:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        break;
                    }
                case 4:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        break;
                    }
                case 5:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        break;
                    }
                case 6:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        characterInfo.AddItem(BuffCritPrefab);
                        break;
                    }
                case 7:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        characterInfo.AddItem(BuffCritPrefab);
                        speed += 0.3f;
                        break;
                    }
                case 8:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        characterInfo.AddItem(BuffCritPrefab);
                        speed += 0.3f;
                        critPercent += 5f;
                        break;
                    }
                case 9:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        characterInfo.AddItem(BuffCritPrefab);
                        speed += 0.3f;
                        critPercent += 5f;
                        critDmgPercent += 15f;
                        break;
                    }
                case 10:
                    {
                        critPercent += 2f;
                        critPercent += 3f;
                        critDmgPercent += 5f;
                        critPercent += 0.3f;
                        critDmgPercent += 10f;
                        characterInfo.AddItem(BuffCritPrefab);
                        speed += 0.3f;
                        critPercent += 5f;
                        critDmgPercent += 15f;
                        //có 30% ti le né tránh khi nhận sát thương
                        evasion += 30;
                        break;
                    }
            }
        }

        SkillTreeBuff();
    }

    private void SkillTreeBuff()
    {
        CharacterInfo_1 player=GetComponent<CharacterInfo_1>();
        player.healthPercent+= healthPercent;
        player.attackPercent+= attackPercent;
        player.speedPercent += speedPercent;
        player.critPercent += critPercent;
        player.critDamagePercent+= critDmgPercent;
        player.reduceDmgTake+= reduceDmgTake;
        player.skillInfor.cdSkill-= player.characterData.skillInfo.cdSkill*reduceCdSkill;
        player.speedPercent+= speed;
        player.evasion+= evasion;

        player.statUpdate();
    }
}
