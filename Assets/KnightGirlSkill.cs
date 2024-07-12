using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightGirlSkill : MonoBehaviour
{
    [SerializeField]
    GameObject effectSkillPrefab;
    [SerializeField]
    float timeDeActiveSkill = 5;

    SkillCooldownUI skillCooldownUI;

    float timer = 0;
    CharacterInfo_1 player;

    private void Start()
    {
        skillCooldownUI = GameObject.FindGameObjectWithTag("SkillCooldown").GetComponent<SkillCooldownUI>();
        player = GetComponentInParent<CharacterInfo_1>();

        skillCooldownUI.SetSkillInfo(player.skillInfor);
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && timer <= 0)
        {
            timer = player.skillInfor.cdSkill;
            skillCooldownUI.SetCooldown(timer);
            Skill();
        }

    }

    private void Skill()
    {
        effectSkillPrefab.SetActive(true);
        StartCoroutine(DeActiveSkill());
        player.isInvincible = true;
        player.HealthByPercent(100);

    }

    private IEnumerator DeActiveSkill()
    {
        yield return new WaitForSeconds(timeDeActiveSkill);
        effectSkillPrefab.SetActive(false);
        player.isInvincible = false;
    }
}
