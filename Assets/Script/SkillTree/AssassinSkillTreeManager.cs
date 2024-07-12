using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssassinSkillTreeManager : MonoBehaviour
{
    public List<Skill> skillList;
    public List<SetSkillInfoHolder> skillInfoHolderList;

    [SerializeField] Text SkillName;
    [SerializeField] Image SkillIcon;
    [SerializeField] Text SkillDescription;
    [SerializeField] GameObject SkillContainer;
    [SerializeField] GameObject ClassLocker;
    [SerializeField] SetSkillPrice skillPrice;
    [SerializeField] GameObject BuyButton;
    [SerializeField] SkillTreeSystemManager skillTreeSystemManager;

    int currentLevel;

    CharacterData characterData;

    public void Set(int level, CharacterData characterData = null)
    {
        this.characterData = skillTreeSystemManager.GetCharacterData();
        currentLevel = level;

        for (int i = 0; i < level; i++)
        {
            skillList[i].isUpgrade = true;
        }

        if (level > 0)
        {
            ClassLocker.SetActive(false);
        }
        else
        {
            ClassLocker.SetActive(true);
        }
    }

    public void SetInfo()
    {
        if (currentLevel != 10)
        {
            SkillContainer.SetActive(skillList[currentLevel].isUpgrade);
            SkillName.text = skillList[currentLevel].skillName;
            SkillDescription.text = skillList[currentLevel].skillDescription;
            SkillIcon.sprite = skillList[currentLevel].skillIcon;
            skillPrice.Set(this.characterData, skillList[currentLevel], 3);
        }
    }

    public void Onclick()
    {
        BuyButton.SetActive(true);
        skillPrice.Set(characterData, skillList[currentLevel], 3);
    }
}
