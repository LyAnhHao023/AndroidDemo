using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text buffName;
    [SerializeField] Text description;
    [SerializeField] Text type;
    [SerializeField] GameObject newText;

    string[] types = { "Vũ khí", "Trang bị", "Chỉ số", "Tiền" };

    public void Set(UpgradeData upgradeData)
    {
        icon.sprite = upgradeData.icon;
        buffName.text = upgradeData.buffName;
        description.text = upgradeData.description;
        switch ((int)upgradeData.upgradeType)
        {
            case 0:
                {
                    type.text = types[0];
                    newText.SetActive(false);
                    buffName.text += string.Format(" Lv. {0}", upgradeData.level + 1);
                }
                break;
            case 1:
                {
                    type.text = types[1];
                    newText.SetActive(false);
                    buffName.text += string.Format(" Lv. {0}", upgradeData.itemsData.level + 1);
                }
                break;
            case 2:
                {
                    type.text = types[0];
                    newText.SetActive(true);
                }
                break;
            case 3:
                {
                    type.text = types[1];
                    newText.SetActive(true);
                }
                break;
            case 4:
                {
                    type.text = types[2];
                    newText.SetActive(false);
                }
                break;
            case 5:
                {
                    type.text = types[3];
                    newText.SetActive(false);
                }
                break;
            default:
                {
                    type.text = "";
                    newText.SetActive(false);
                }
                break;
        }
    }
}
