using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooldownUI : MonoBehaviour
{
    [SerializeField] Image cooldown;
    [SerializeField] Image skillIcon;
    [SerializeField] GameObject skillOverlay;

    float cooldownTimer;
    float cooldownTime;

    private void Update()
    {
        ApplyCooldown();
    }

    public void SetCooldown(float time)
    {
        cooldownTime = time;
        cooldownTimer = time;
        cooldown.fillAmount = cooldownTimer;
        skillOverlay.SetActive(false);
    }

    public void SetSkillInfo(SkillInfo skillInfo)
    {
        skillIcon.sprite = skillInfo.Icon;
    }

    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer < 0 )
        {
            cooldown.fillAmount = 0;
            skillOverlay.SetActive(false);
        }
        else
        {
            cooldown.fillAmount = cooldownTimer / cooldownTime;
            skillOverlay.SetActive(true);
        }
    }
}
