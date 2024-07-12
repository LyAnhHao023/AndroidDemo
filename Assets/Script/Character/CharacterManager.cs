using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] public List<CharacterData> characterDatas;

    private void Start()
    {
        SetCharacterData();
    }

    public void SetCharacterData()
    {
        foreach (var characterData in characterDatas)
        {
            if (characterData.name == "FirstCharDev")
            {
                characterData.stats.maxHealth = 100;
                characterData.stats.strenght = 10;
                characterData.stats.speed = 3;
                characterData.stats.crit = 10;
                characterData.stats.critDmg = 1.5f;
            }
            if (characterData.name == "KnightGirl")
            {
                characterData.stats.maxHealth = 120;
                characterData.stats.strenght = 3;
                characterData.stats.speed = 3;
                characterData.stats.crit = 10;
                characterData.stats.critDmg = 1.5f;
            }
            if (characterData.name == "OdleHero")
            {
                characterData.stats.maxHealth = 120;
                characterData.stats.strenght = 5;
                characterData.stats.speed = 3;
                characterData.stats.crit = 10;
                characterData.stats.critDmg = 1.5f;
            }
            if (characterData.name == "Samurai")
            {
                characterData.stats.maxHealth = 80;
                characterData.stats.strenght = 4;
                characterData.stats.speed = 4;
                characterData.stats.crit = 20;
                characterData.stats.critDmg = 1.6f;
            }
        }
    }
}
