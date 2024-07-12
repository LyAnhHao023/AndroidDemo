using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSkill : MonoBehaviour
{

    [SerializeField]
    GameObject effectSkillPrefab;

    [SerializeField] float timeToDeActiveSkill = 5f;

    SkillCooldownUI skillCooldownUI;


    float timer = 0;
    SkillInfo skillInfor;
    CharacterStats characterStats;
    SkillButton skillButton;

    private void Start()
    {
        skillButton = GameObject.FindGameObjectWithTag("SkillButton").GetComponent<SkillButton>();

        skillCooldownUI = GameObject.FindGameObjectWithTag("SkillCooldown").GetComponent<SkillCooldownUI>();
        skillInfor = GetComponentInParent<CharacterInfo_1>().skillInfor;
        characterStats = GetComponentInParent<CharacterInfo_1>().characterStats;

        skillCooldownUI.SetSkillInfo(skillInfor);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (skillButton.isUseSkill && timer <= 0)
        {
            timer = skillInfor.cdSkill;
            skillCooldownUI.SetCooldown(timer);
            Skill();
            skillButton.isUseSkill=false;
        }
        else
        {
            skillButton.isUseSkill = false;
        }

    }
    private void Skill()
    {
        effectSkillPrefab.GetComponent<effectSkillOdleHero>().SetDmg(characterStats, skillInfor.strenght, transform.position);
        effectSkillPrefab.SetActive(true);
        Invoke("DeActiveSkil", timeToDeActiveSkill);

    }

    private void DeActiveSkil()
    {
        effectSkillPrefab.SetActive(false);
    }
}
