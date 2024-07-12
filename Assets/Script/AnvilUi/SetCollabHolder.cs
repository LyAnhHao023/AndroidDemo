using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCollabHolder : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Text Name;
    [SerializeField] Text Description;

    public void Set(UpgradeData upgradeData)
    {
        Icon.sprite = upgradeData.icon;
        Name.text = upgradeData.buffName;
        Description.text = upgradeData.description;
    }
}
