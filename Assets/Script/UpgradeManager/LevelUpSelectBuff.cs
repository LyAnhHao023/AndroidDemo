using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Services.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public class WeightedItem
{
    public int type;
    public float weight;
    public bool active;

    public WeightedItem(int type, float weight, bool active = true)
    {
        this.type = type;
        this.weight = weight;
        this.active = active;
    }
}

public class LevelUpSelectBuff : MonoBehaviour
{
    [SerializeField] List<UpgradeData> upgradeList;
    [SerializeField] CharacterInfo_1 characterInfo;

    public List<UpgradeData> weaponList = new List<UpgradeData>();
    public List<UpgradeData> itemList = new List<UpgradeData>();
    public List<UpgradeData> weaponUpgradeList = new List<UpgradeData>();
    public List<UpgradeData> itemUpgradeList = new List<UpgradeData>();
    public List<UpgradeData> statUpgrade = new List<UpgradeData>();
    public UpgradeData GainCoin;
    public List<UpgradeData> collabList = new List<UpgradeData>();

    public List<UpgradeData> weaponAcquiredList = new List<UpgradeData>();
    public List<UpgradeData> itemAcquiredList = new List<UpgradeData>();

    List<WeightedItem> items = new List<WeightedItem>
    {
        new WeightedItem(0, 2f), //GainCoin
        new WeightedItem(1, 13f), //Stat Upgrade
        new WeightedItem(2, 35f), //Upgrade
        new WeightedItem(3, 50f) //Unlock
    };

    UpgradeData randomUp;

    int upgradeCount;

    float totalWeight;

    int totalWeapon;
    int totalItem = 5;

    public int GetRandomType(List<WeightedItem> items)
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
            if (item.active)
            {
                weights += item.weight;

                if (randomValue <= weights)
                {
                    return item.type;
                }
            }
        }

        return -1;
    }

    public void UpdateRate(int type, bool reset = false)
    {
        foreach (var item in items)
        {
            if(type == item.type)
            {
                item.active = reset;
                break;
            }
        }
    }

    private void Awake()
    {
        totalWeapon = 5;

        foreach (var item in upgradeList)
        {
            item.maxed = false;
            item.acquired = false;
            item.level = 1;
            item.overLevel = 0;

            switch ((int)item.upgradeType)
            {
                case 0: //WeaponUpgrade
                    {
                        weaponUpgradeList.Add(item);
                    }
                    break;
                case 1: //ItemUpgrade
                    {
                        itemUpgradeList.Add(item);
                    }
                    break;
                case 2: //WeaponUnlock
                    {
                        weaponList.Add(item);
                    }
                    break;
                case 3: //ItemUnlock
                    {
                        itemList.Add(item);
                    }
                    break;
                case 4: //StatUpgrade
                    {
                        statUpgrade.Add(item);
                    }
                    break;
                case 5: //GainCoin
                    {
                        GainCoin = item;
                    }
                    break;
                case 6: //Collab
                    {
                        collabList.Add(item);
                        item.maxed = true;
                    }
                    break;
            }
        }

        foreach (var item in weaponUpgradeList)
        {
            item.description = item.UpgradeInfos[1].description;
            //item.weaponData.stats = item.UpgradeInfos[0].stats;
        }

        upgradeCount = upgradeList.Count;
    }

    public List<UpgradeData> GetUpgrades(int count)
    {
        List<UpgradeData> upgradeList = new List<UpgradeData>();

        if (count > upgradeCount)
        {
            count = upgradeCount;
        }

        do
        {
            int type = GetRandomType(items);

            switch (type)
            {
                case 0: //GainCoin
                    {
                        randomUp = GainCoin;
                    }
                    break;
                case 1: //Stat Upgrade
                    {
                        randomUp = statUpgrade[Random.Range(0, statUpgrade.Count)];
                    }
                    break;
                case 2: //Upgrade
                    {
                        int randomUpType = -1;

                        int weaponNum = weaponAcquiredList.Count<UpgradeData>(item => item.level == 7); //so luong vu khi chua dat level toi da (max = 5)

                        int itemNum = itemAcquiredList.Count<UpgradeData>(item => item.maxed == true); //so luong trang bi chua dat level toi da (max = 5)

                        if ((weaponNum == weaponAcquiredList.Count && itemNum == itemAcquiredList.Count) || (weaponNum == weaponAcquiredList.Count && itemNum == 0))
                        {
                            UpdateRate(type);
                            break;
                        }

                        if (weaponNum != weaponAcquiredList.Count && itemNum != itemAcquiredList.Count)
                        {
                            randomUpType = Random.Range(0, 2);
                        }
                        else
                        {
                            if (weaponNum == weaponAcquiredList.Count)
                            {
                                randomUpType = 1;
                            }

                            if (itemNum == itemAcquiredList.Count)
                            {
                                randomUpType = 0;
                            }
                        }

                        switch (randomUpType)
                        {
                            case 0: //Weapon
                                {
                                    List<UpgradeData> upgradeDatas = null;
                                    upgradeDatas = weaponAcquiredList.FindAll(item => item.level < 7);

                                    if(upgradeDatas != null)
                                    {
                                        UpgradeData randomUpdate = upgradeDatas[Random.Range(0, upgradeDatas.Count)];

                                        UpgradeData upgradeData = null;

                                        upgradeData = weaponUpgradeList.Find(item => item.weaponData.name == randomUpdate.weaponData.name && item.level < 7);

                                        if (upgradeData != null)
                                        {
                                            randomUp = upgradeData;
                                        }
                                    }
                                }
                                break;
                            case 1: //Item
                                {
                                    List<UpgradeData> upgradeDatas = null;
                                    upgradeDatas = itemAcquiredList.FindAll(item => item.maxed == false);

                                    if(upgradeDatas != null)
                                    {
                                        UpgradeData randomUpdate = upgradeDatas[Random.Range(0, upgradeDatas.Count)];

                                        UpgradeData upgradeData = null;

                                        upgradeData = itemUpgradeList.Find(item => item.itemsData.name == randomUpdate.itemsData.name);

                                        if (upgradeData != null)
                                        {
                                            randomUp = upgradeData;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    break;
                case 3: //Unlock
                    {
                        int randomUpType = -1;

                        if ((weaponAcquiredList.Count == totalWeapon || weaponAcquiredList.Count<UpgradeData>(item => item.acquired == true) == 14) && itemAcquiredList.Count == 5)
                        {
                            UpdateRate(type);
                            break;
                        }

                        if (weaponAcquiredList.Count < totalWeapon && itemAcquiredList.Count < 5)
                        {
                            randomUpType = Random.Range(0, 2);
                        }
                        else
                        {
                            if (weaponAcquiredList.Count == totalWeapon)
                            {
                                randomUpType = 1;
                            }

                            if (itemAcquiredList.Count == 5)
                            {
                                randomUpType = 0;
                            }
                        }

                        switch (randomUpType)
                        {
                            case 0: //Weapon
                                {
                                    List<UpgradeData> upgradeDatas = weaponList.FindAll(item => item.acquired == false);

                                    randomUp = upgradeDatas[Random.Range(0, upgradeDatas.Count)];
                                }
                                break;
                            case 1: //Item
                                {
                                    List<UpgradeData> upgradeDatas = itemList.FindAll(item => item.acquired == false);

                                    randomUp = upgradeDatas[Random.Range(0, upgradeDatas.Count)];
                                }
                                    break;
                        }
                    }
                    break;
            }

            if (!upgradeList.Contains(randomUp) && randomUp != null)
            {
                if (!randomUp.acquired)
                {
                    upgradeList.Add(randomUp);
                    randomUp = null;
                }
            }
        } while (upgradeList.Count < count);

        return upgradeList;
    }

    public void WeaponAcquired(UpgradeData weaponA)
    {
        weaponAcquiredList.Add(weaponA);
        weaponA.acquired = true;
        UpdateRate(2, true);
    }

    public void WeaponNextUpgradeInfo(UpgradeData weaponA)
    {
        if ((int)weaponA.upgradeType == 2)
        {
            UpgradeData data = null;

            data = weaponUpgradeList.Find(item => item.weaponData == weaponA.weaponData);

            if(data != null)
            {
                if(data.level < 7)
                {
                    data.acquired = false;
                    data.level = data.UpgradeInfos[data.level].level;
                    if (data.level == 7)
                    {
                        weaponA.maxed = true;
                        //RemoveEquipWeapon(weaponA);
                    }
                    else
                    {
                        data.description = data.UpgradeInfos[data.level].description;
                    }
                }
            }
        }
        else
        {
            if (weaponA.level < 7)
            {
                weaponA.acquired = false;
                weaponA.level = weaponA.UpgradeInfos[weaponA.level].level;
                if (weaponA.level == 7)
                {
                    UpgradeData data = null;

                    data = weaponAcquiredList.Find(item => item.weaponData == weaponA.weaponData);

                    if(data != null)
                    {
                        data.maxed = true;
                        //RemoveEquipWeapon(data);
                    }
                    return;
                }
                weaponA.description = weaponA.UpgradeInfos[weaponA.level].description;
            }
        }
    }

    public void ItemAcquired(UpgradeData itemA)
    {
        itemAcquiredList.Add(itemA);
        itemA.acquired = true;
        UpdateRate(3, true);
    }

    public void ItemNextUpgradeInfo(UpgradeData itemA)
    {
        if ((int)itemA.upgradeType == 3)
        {
            UpgradeData data = null;

            data = itemUpgradeList.Find(item => item.itemsData == itemA.itemsData);

            if(data != null)
            {
                if(data.level < data.UpgradeInfos.Count)
                {
                    data.acquired = false;
                    itemA.level += 1;
                    data.itemsData.level = data.UpgradeInfos[data.itemsData.level].level;
                    if(data.level == data.UpgradeInfos.Count)
                    {
                        itemA.maxed = true;
                        data.maxed = true;
                        //RemoveEquipItem(data);
                    }
                    else
                    {
                        data.description = data.UpgradeInfos[data.itemsData.level].description;
                    }
                }
            }
        }
        else
        {
            if (itemA.itemsData.level < itemA.UpgradeInfos.Count)
            {
                itemA.acquired = false;
                itemA.itemsData.level = itemA.UpgradeInfos[itemA.itemsData.level].level;
                if (itemA.itemsData.level == itemA.UpgradeInfos.Count)
                {
                    UpgradeData data = null;

                    data = itemAcquiredList.Find(item => item.itemsData == itemA.itemsData);

                    if(data != null)
                    {
                        data.maxed = true;
                        //RemoveEquipItem(data);
                    }

                    itemA.maxed = true;

                    return;
                }
                itemA.description = itemA.UpgradeInfos[itemA.itemsData.level].description;
            }
        }
    }

    /*public void RemoveEquipWeapon(UpgradeData upgradeData)
    {
        UpgradeData removeItem = null;

        removeItem = weaponAcquiredList.Find(item => item.weaponData == upgradeData.weaponData);

        if(removeItem != null)
        {
            weaponAcquiredList.Remove(removeItem);
        }
    }

    public void RemoveEquipItem(UpgradeData upgradeData)
    {
        UpgradeData removeItem = null;

        removeItem = itemAcquiredList.Find(item => item.itemsData == upgradeData.itemsData);

        if(removeItem != null)
        {
            itemAcquiredList.Remove(removeItem);
        }
    }*/

    public void AddCollabWeapon(UpgradeData upgradeData)
    {
        weaponAcquiredList.Add(upgradeData);
        upgradeData.acquired = true;
        upgradeData.maxed = true;
        totalWeapon += 2;
        UpdateRate(3, true);

        if(totalWeapon >= 15)
        {
            totalWeapon = 15;
        }
    }
}
