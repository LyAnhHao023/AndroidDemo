using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetItem : MonoBehaviour
{
    [SerializeField] GameObject IconHodler;
    [SerializeField] Image Icon;
    [SerializeField] Button IconButton;

    [SerializeField] GameObject inventoryItemInfo;
    [SerializeField] SetInventoryItemInfo setInventoryItemInfo;
    [SerializeField] ChanceSet chanceSet;

    UpgradeData upgradeData;

    bool type;

    private void Start()
    {
        IconButton.onClick.AddListener(Onclick);
    }

    public void SetWeaponSlot(UpgradeData weaponData)
    {
        upgradeData = weaponData;
        Icon.sprite = weaponData.icon;
        IconHodler.SetActive(true);
        type = true;
    }

    public void SetItemSlot(UpgradeData itemData)
    {
        upgradeData = itemData;
        Icon.sprite = itemData.icon;
        IconHodler.SetActive(true);
        type = false;
    }

    void Onclick()
    {
        setInventoryItemInfo.Set(upgradeData);
        chanceSet.SetUpgrade(upgradeData, type);
        inventoryItemInfo.SetActive(true);
    }
}
