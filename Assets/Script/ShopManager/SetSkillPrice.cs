using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillPrice : MonoBehaviour
{
    [SerializeField] Text SkillPrice;
    [SerializeField] GameObject ButtonOverlay;
    [SerializeField] ShopManager shopManager;
    [SerializeField] SkillTreeSystemManager skillTreeSystemManager;
    
    public CharacterData characterData;

    private void Awake()
    {
        totalCoins = PlayerPrefs.GetInt("Coins", 0);
    }

    private void Update()
    {
        totalCoins = PlayerPrefs.GetInt("Coins", 0);
    }

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

        PriceCheck();

        MaxLevelCheck();
    }

    private void PriceCheck()
    {
        Debug.Log(skill.prices.ToString());

        if (totalCoins >= skill.prices)
        {
            ButtonOverlay.SetActive(false);
            transform.GetComponent<Button>().enabled = true;
        }
        else
        {
            ButtonOverlay.SetActive(true);
            transform.GetComponent<Button>().enabled = false;
        }
    }

    private void MaxLevelCheck()
    {
        if(characterData.skillTree.level >= 10)
        {
            SkillPrice.text = "MAX";
            ButtonOverlay.SetActive(true);
            transform.GetComponent<Button>().enabled = false;
        }
    }

    public void Onclick()
    {
        totalCoins -= skill.prices;

        PlayerPrefs.SetInt("Coins", totalCoins);
        PlayerPrefs.Save();

        shopManager.Set();

        characterData.skillTree.type = type;
        characterData.skillTree.level += 1;

        PlayerPrefs.SetInt("TreeType" + characterData.name, characterData.skillTree.type);
        PlayerPrefs.Save();
        PlayerPrefs.SetInt("TreeLevel" + characterData.name, characterData.skillTree.level);
        PlayerPrefs.Save();

        PriceCheck();
        MaxLevelCheck();

        skillTreeSystemManager.SetSkillInfo();
    }
}
