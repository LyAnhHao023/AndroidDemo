using NUnit.Compatibility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnvilUpgradeResult : MonoBehaviour
{
    [SerializeField] Text BuffResult;
    [SerializeField] Text ResultsText;
    [SerializeField] GameObject ResultText;
    [SerializeField] GameObject ResultButton;
    [SerializeField] GameObject ImageAnimation;
    [SerializeField] Animator animator;
    [SerializeField] Image BuffIcon;
    [SerializeField] CharacterInfo_1 Character;

    UpgradeData data;
    bool result;
    int upgradePrice;
    bool type;

    private void Start()
    {
        ResultText.SetActive(false);
        ResultButton.SetActive(false);
    }

    public void Set(UpgradeData upgradeData, int succesed, bool type, int prices)
    {
        data = upgradeData;
        result = succesed == 0 ? true : false;
        upgradePrice = prices;
        this.type = type;

        BuffIcon.sprite = upgradeData.icon;

        if(result)
        {
            Character.AddUpgrade(data, type);
        }

        StartCoroutine(WaitForAnimationEnd());
    }

    private IEnumerator WaitForAnimationEnd()
    {
        animator.SetBool("Upgrade", true);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("UpgradeAnimation"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        animator.SetBool("Upgrade", false);

        SetResult();
    }

    private void SetResult()
    {
        ImageAnimation.SetActive(false);
        if (result)
        {
            Character.coins -= upgradePrice;
        }

        string overLevelText = "";

        if (data.maxed && type && data.overLevel != 0)
        {
            overLevelText = string.Format("+{0}", data.overLevel);
        }

        ResultsText.text = result ? "THÀNH CÔNG" : "THẤT BẠI";
        BuffResult.text = string.Format("{0} Lv.{1}" + overLevelText, data.buffName, type ? data.level : data.itemsData.level);
        ResultText.SetActive(true);
        ResultButton.SetActive(true);
    }
}
