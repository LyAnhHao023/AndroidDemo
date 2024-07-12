using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    [SerializeField] Text levelText;
    public Slider slider;

    public void SetMaxExp(int level, int maxExpValue)
    {
        slider.maxValue = maxExpValue;
        slider.value = 0;
        levelText.text = string.Format("Lv. {0}", level);
    }

    public void SetExp(int exp)
    {
        slider.value = exp;
    }
}
