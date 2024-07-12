using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillPrice : MonoBehaviour
{
    [SerializeField] Text SkillPrice;
    [SerializeField] GameObject ButtonOverlay;
    [SerializeField] ShopManager shopManager;
    [SerializeField] SkillTreeSystemManager skillTreeSystemManager;
    
    public CharacterData characterData;

    public void SetCharacterData(CharacterData characterData)
    {
        this.characterData = characterData;
    }

    Skill skill;

    int totalCoins;

    int type = 0;

    public void Set(CharacterData characterData, Skill skill, int type)
    {
        this.type = type;
        this.characterData = skillTreeSystemManager.GetCharacterData();
        this.skill = skill;
        SkillPrice.text = skill.prices.ToString();

        totalCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void PriceCheck()
    {
        if(skill.prices >= totalCoins)
        {
            ButtonOverlay.SetActive(false);
            transform.GetComponent<Button>().enabled = false;
        }
        else
        {
            ButtonOverlay.SetActive(true);
            transform.GetComponent<Button>().enabled = true;
        }
    }

    public void Onclick()
    {
        totalCoins -= skill.prices;

        PriceCheck();

        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();

        shopManager.Set();

        characterData.skillTree.type = type;
        characterData.skillTree.level += 1;

        skillTreeSystemManager.SetSkillInfo();
    }
}
