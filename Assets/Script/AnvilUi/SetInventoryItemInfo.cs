using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetInventoryItemInfo : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] GameObject IconHolder;
    [SerializeField] Text Name;

    public void Set(UpgradeData upgradeData)
    {
        IconHolder.SetActive(true);
        Icon.sprite = upgradeData.icon;

        string OverLevel = "";

        if(upgradeData.maxed && upgradeData.overLevel > 0)
        {
            OverLevel = string.Format("+{0}", upgradeData.overLevel);
        }

        Name.text = string.Format("{0} Lv.{1} {2}", upgradeData.buffName, upgradeData.level, OverLevel);
    }
}
