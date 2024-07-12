using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountSys : MonoBehaviour
{
    [SerializeField] Text killText;
    [SerializeField] Text coinText;

    public void SetCoinCount(int coins)
    {
        coinText.text = coins.ToString();
    }

    public void SetKillCount(int kills)
    {
        killText.text = kills.ToString();
    }
}
