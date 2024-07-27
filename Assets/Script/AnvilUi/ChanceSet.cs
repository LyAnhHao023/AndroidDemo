using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ChanceSet : MonoBehaviour
{
    [SerializeField] GameObject Overlay;
    [SerializeField] Button UpgradeButon;
    [SerializeField] Text Chance;
    [SerializeField] Text Price;
    [SerializeField] GameObject ChanceHolder;
    [SerializeField] CharacterInfo_1 Character;
    [SerializeField] GameObject ResultTable;
    [SerializeField] AnvilUpgradeResult upgradeResult;

    int baseUpgradePrice = 100;
    float baseChance = 100f;
    float succesChance;
    int upgradePrice;

    UpgradeData upgradeData;
    bool type;

    float totalWeight;

    List<WeightedItem> items = new List<WeightedItem>
    {
        new WeightedItem(0, 100f), //succes
        new WeightedItem(1, 0f), //fail
    };

    int GetRandomType(List<WeightedItem> items)
    {
        totalWeight = 0;

        foreach (var item in items)
        {
            if (item.active)
                totalWeight += item.weight;
        }

        float randomValue = Random.Range(0, totalWeight);

        float weights = 0f;

        foreach (var item in items)
        {
            weights += item.weight;

            if (randomValue <= weights)
            {
                return item.type;
            }
        }

        return -1;
    }

    private void Start()
    {
        ResultTable.SetActive(false);
        ChanceHolder.SetActive(false);

        UpgradeButon.onClick.AddListener(Onclick);
    }

    void UpdateRate()
    {
        foreach (var item in items)
        {
            switch (item.type)
            {
                case 0:
                    {
                        item.weight = succesChance;
                    }
                    break;
                case 1:
                    {
                        item.weight = 100 - succesChance;
                    }
                    break;
            }
        }
    }

    public void SetUpgrade(UpgradeData data, bool type)//true = weapon, false = item
    {
        upgradeData = data;
        this.type = type;
        if (type)
        {
            upgradePrice = baseUpgradePrice * data.level;

            if(data.overLevel > 0)
            {
                upgradePrice = baseUpgradePrice * data.level + baseUpgradePrice * data.overLevel;
            }
        }
        else
        {
            upgradePrice = baseUpgradePrice * data.itemsData.level;
        }

        SetOverlay(type);
        Price.text = upgradePrice.ToString();
        ChanceCal();
        Chance.text = string.Format("{0}%", succesChance);

        ChanceHolder.SetActive(true);
        ResultTable.SetActive(false);
    }

    private void SetOverlay(bool type)
    {
        if (Character.coins < upgradePrice)
        {
            if(type)
            {
                Overlay.SetActive(true);
            }
            else
            {
                if (upgradeData.maxed)
                    Overlay.SetActive(true);
                else
                    Overlay.SetActive(false);
            }
        }
        else
            Overlay.SetActive(false);
    }

    void ChanceCal()
    {
        if(!upgradeData.maxed)
        {
            succesChance = baseChance;
        }
        else
        {
            succesChance = baseChance - (upgradeData.overLevel - 1) * 15;

            if(succesChance < 25)
            {
                succesChance = 25f;
            }

            if(succesChance > 100)
            {
                succesChance = 100;
            }
        }

        UpdateRate();
    }

    void Onclick()
    {
        int succesed = GetRandomType(items);

        ResultTable.SetActive(true);

        upgradeResult.Set(upgradeData, succesed, type, upgradePrice);
    }
}
