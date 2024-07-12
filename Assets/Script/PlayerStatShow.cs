using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatShow : MonoBehaviour
{
    [SerializeField] Text healthText;
    [SerializeField] Text attackText;
    [SerializeField] Text speedText;
    [SerializeField] Text critText;
    /*[SerializeField] Text pickUpRadiusText;*/

    public void SetHealth(int health)
    {
        healthText.text = health.ToString();
    }

    public void SetAttack(int attack)
    {
        attackText.text = attack.ToString();
    }

    public void SetSpeed(int speed)
    {
        speedText.text = speed.ToString();
    }

    public void SetCrit(float crit)
    {
        critText.text = string.Format("{0}%", crit);
    }

    /*public void SetRadius(int pickUpRadius)
    {
        pickUpRadiusText.text = pickUpRadius.ToString();
    }*/
}
