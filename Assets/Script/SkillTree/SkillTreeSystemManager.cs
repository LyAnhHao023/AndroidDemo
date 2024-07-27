using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeSystemManager : MonoBehaviour
{
    [SerializeField] WizardSkillTreeManager wizardSkillTree;
    [SerializeField] WarriorSkillTreeManager warriorSkillTree;
    [SerializeField] AssassinSkillTreeManager assassinSkillTree;
    [SerializeField] Button wizardSkillTreeButton;
    [SerializeField] Button warriorSkillTreeButton;
    [SerializeField] Button assassinSkillTreeButton;

    [SerializeField] GameObject InfoHolder;
    [SerializeField] GameObject BuyButton;

    CharacterData characterData;

    public CharacterData GetCharacterData() { return characterData; }

    private void Start()
    {
        warriorSkillTree.Set(0);
        wizardSkillTree.Set(0);
        assassinSkillTree.Set(0);
    }

    public void ResetSkillTree()
    {
        characterData.skillTree.type = 0;
        characterData.skillTree.level = 0;

        SetSkillTree(characterData);
        Set();
    }

    public void SetSkillTree(CharacterData characterData)
    {
        this.characterData = characterData;
        BuyButton.SetActive(true);
        InfoHolder.SetActive(false);
        BuyButton.SetActive(false);
    }

    public void Set()
    {
        switch (characterData.skillTree.type)
        {
            case 0:
                {
                    warriorSkillTree.Set(0);
                    wizardSkillTree.Set(0);
                    assassinSkillTree.Set(0);
                    warriorSkillTreeButton.enabled = true;
                    wizardSkillTreeButton.enabled = true;
                    assassinSkillTreeButton.enabled = true;
                }
                break;
            case 1:
                {
                    warriorSkillTree.Set(characterData.skillTree.level, this.characterData);
                    wizardSkillTree.Set(0);
                    assassinSkillTree.Set(0);
                    warriorSkillTreeButton.enabled = true;
                    wizardSkillTreeButton.enabled = false;
                    assassinSkillTreeButton.enabled = false;
                }
                break;
            case 2:
                {
                    warriorSkillTree.Set(0);
                    wizardSkillTree.Set(characterData.skillTree.level, this.characterData);
                    assassinSkillTree.Set(0);
                    warriorSkillTreeButton.enabled = false;
                    wizardSkillTreeButton.enabled = true;
                    assassinSkillTreeButton.enabled = false;
                }
                break;
            case 3:
                {
                    warriorSkillTree.Set(0);
                    wizardSkillTree.Set(0);
                    assassinSkillTree.Set(characterData.skillTree.level, this.characterData);
                    warriorSkillTreeButton.enabled = false;
                    wizardSkillTreeButton.enabled = false;
                    assassinSkillTreeButton.enabled = true;
                }
                break;
        }
    }

    public void SetSkillInfo()
    {
        switch (characterData.skillTree.type)
        {
            case 1:
                {
                    warriorSkillTree.Set(characterData.skillTree.level, this.characterData);
                    warriorSkillTree.SetInfo();
                    wizardSkillTree.Set(0);
                    assassinSkillTree.Set(0);
                    warriorSkillTreeButton.enabled = true;
                    wizardSkillTreeButton.enabled = false;
                    assassinSkillTreeButton.enabled = false;
                }
                break;
            case 2:
                {
                    warriorSkillTree.Set(0);
                    wizardSkillTree.Set(characterData.skillTree.level, this.characterData);
                    wizardSkillTree.SetInfo();
                    assassinSkillTree.Set(0);
                    warriorSkillTreeButton.enabled = false;
                    wizardSkillTreeButton.enabled = true;
                    assassinSkillTreeButton.enabled = false;
                }
                break;
            case 3:
                {
                    warriorSkillTree.Set(0);
                    wizardSkillTree.Set(0);
                    assassinSkillTree.Set(characterData.skillTree.level, this.characterData);
                    assassinSkillTree.SetInfo();
                    warriorSkillTreeButton.enabled = false;
                    wizardSkillTreeButton.enabled = false;
                    assassinSkillTreeButton.enabled = true;
                }
                break;
        }
    }
}
