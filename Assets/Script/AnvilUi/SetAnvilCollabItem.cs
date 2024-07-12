using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAnvilCollabItem : MonoBehaviour
{
    [SerializeField] GameObject IconHodler;
    [SerializeField] GameObject Overlay;
    [SerializeField] Image Icon;
    [SerializeField] Button IconButton;

    [SerializeField] SetSelectedItem setSelectedItem1;
    [SerializeField] SetSelectedItem setSelectedItem2;

    UpgradeData upgradeData;

    bool type;

    private void Start()
    {
        IconButton.onClick.AddListener(Onclick);
    }

    public void SetWeaponSlot(UpgradeData weaponData)
    {
        if(weaponData.maxed)
            Overlay.SetActive(false);
        else
            Overlay.SetActive(true);
        upgradeData = weaponData;
        Icon.sprite = weaponData.icon;
        IconHodler.SetActive(true);
        type = true;
    }

    public void SetItemSlot(UpgradeData itemData)
    {
        if (itemData.maxed)
            Overlay.SetActive(false);
        else
            Overlay.SetActive(true);
        upgradeData = itemData;
        Icon.sprite = itemData.icon;
        IconHodler.SetActive(true);
        type = false;
    }

    void Onclick()
    {
        if(setSelectedItem1.GetData() == null)
        {
            Overlay.SetActive(true);
            setSelectedItem1.Set(upgradeData, Overlay);
        }
        else if (setSelectedItem2.GetData() == null)
        {
            Overlay.SetActive(true);
            setSelectedItem2.Set(upgradeData, Overlay);
        }
    }
}
