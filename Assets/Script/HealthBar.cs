using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Text healthText;
    public Slider slider;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        healthText.text = string.Format("{0}/{1}", slider.value, slider.maxValue);
    }

    public void SetHealth(int health, bool isSlow = false)
    {
        slider.value = health;
        if(!isSlow)
            healthText.text = string.Format("{0}/{1}", slider.value, slider.maxValue);
    }
}
