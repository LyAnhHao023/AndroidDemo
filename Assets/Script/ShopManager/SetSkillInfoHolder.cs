using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSkillInfoHolder : MonoBehaviour
{
    [SerializeField] Skill skill;
    public int Key;
    public Image Icon;
    public Text Name;
    public Text Description;
    public GameObject Locker;

    public void PressSkillButton()
    {
        Icon.sprite = skill.skillIcon;
        Name.text = skill.skillName;
        Description.text = skill.skillDescription;

        Locker.SetActive(!skill.isUpgrade);
    }
}
