using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSlotManager : MonoBehaviour
{
    public List<SetItem> weapons;
    public List<SetItem> items;

    public void SetWeapon(List<UpgradeData> upgradeDatas)
    {
        for(int i = 0; i < upgradeDatas.Count; i++)
        {
            weapons[i].SetWeaponSlot(upgradeDatas[i]);
        }
    }

    public void SetPassiveItem(List<UpgradeData> upgradeDatas)
    {
        for (int i = 0; i < upgradeDatas.Count; i++)
        {
            items[i].SetItemSlot(upgradeDatas[i]);
        }
    }
}
