using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    [SerializeField] List<InventorySlot> weaponSlots = new List<InventorySlot>();
    [SerializeField] List<InventorySlot> itemSlots = new List<InventorySlot>();
    [SerializeField] Sprite baseWeaponIcon;

    public void WeaponSlotUpdate(List<UpgradeData> inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            weaponSlots[i].SetWeaponSlot(inventorySlots[i]);
        }

        for(int i = inventorySlots.Count; i < weaponSlots.Count; i++)
        {
            weaponSlots[i].ClearSlot(baseWeaponIcon);
        }
    }

    public void ItemSlotUpdate(List<UpgradeData> inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            itemSlots[i].SetItemSlot(inventorySlots[i].itemsData, inventorySlots[i].icon);
        }
    }
}
