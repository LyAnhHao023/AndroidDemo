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
    [SerializeField] GameObject UpgradeEffect;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] Image BuffIcon;
    [SerializeField] CharacterInfo_1 Character;
    [SerializeField] AudioManager Audio;

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
        UpgradeEffect.SetActive(true);
        Audio.ClearSFX();
        Effect.Play();
        Audio.PlaySFX(Audio.UpgradeAnvil);

        while (Effect.isPlaying)
        {
            yield return null;
        }

        UpgradeEffect.SetActive(false);

        SetResult();
    }

    private void SetResult()
    {
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
