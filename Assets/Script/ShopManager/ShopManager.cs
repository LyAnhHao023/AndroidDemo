using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Text coinText;
    [SerializeField] Text pointsText;
    [SerializeField] GameObject goodsInfoHolder;

    [SerializeField] GameObject skillTreeSystemManager;

    [SerializeField] GameObject Reset;
    [SerializeField] GameObject ResetOverlay;

    public GameObject goodsInfo()
    {
        return goodsInfoHolder;
    }

    public GameObject SkillTree()
    {
        return skillTreeSystemManager;
    }

    public GameObject ResetButton()
    {
        return Reset;
    }

    public GameObject ResetButtonOverlay()
    {
        return ResetOverlay;
    }

    int totalCoin;
    int totalPoint;

    private void Awake()
    {
        /*PlayerPrefs.SetInt("Coins", 100000);
        PlayerPrefs.Save();*/
    }

    private void Update()
    {
        Set();
    }

    public void Set()
    {
        totalCoin = PlayerPrefs.GetInt("Coins", 0);
        coinText.text = totalCoin.ToString();
        totalPoint = PlayerPrefs.GetInt("TotalPoints", 0);
        pointsText.text = string.Format("{0} PT", totalPoint);
    }
}
