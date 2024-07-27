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

    UpgradeData upgradeData = null;

    bool type;

    private void Start()
    {
        IconButton.onClick.AddListener(Onclick);
    }

    private void Update()
    {
        if (upgradeData == null)
        {
            IconHodler.SetActive(false);
            IconButton.enabled = false;
        }
    }

    public void ClearSlot()
    {
        upgradeData = null;
    }

    public void SetWeaponSlot(UpgradeData weaponData)
    {
        upgradeData = weaponData;
        Icon.sprite = weaponData.icon;
        IconHodler.SetActive(true);
        IconButton.enabled = true;
        type = true;
    }

    public void SetItemSlot(UpgradeData itemData)
    {
        upgradeData = itemData;
        Icon.sprite = itemData.icon;
        IconHodler.SetActive(true);
        IconButton.enabled = true;
        type = false;
    }

    void Onclick()
    {
        setInventoryItemInfo.Set(upgradeData);
        chanceSet.SetUpgrade(upgradeData, type);
        inventoryItemInfo.SetActive(true);
    }
}
