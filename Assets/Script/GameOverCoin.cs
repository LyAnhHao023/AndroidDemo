using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverCoin : MonoBehaviour
{
    [SerializeField] Text coinText;

    public void SetCoinGain(int coins)
    {
        Debug.Log(coins);
        coinText.text = string.Format("+ {0}", coins);
    }
}
