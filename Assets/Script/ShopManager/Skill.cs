using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public string skillName;
    public Sprite skillIcon;

    public string skillDescription;
    public int prices;
    public bool isUpgrade;

    [SerializeField] GameObject Locker;
    [SerializeField] Image Icon;

    private void Start()
    {
        Locker.SetActive(!isUpgrade);
        Icon.sprite = skillIcon;
    }
}
