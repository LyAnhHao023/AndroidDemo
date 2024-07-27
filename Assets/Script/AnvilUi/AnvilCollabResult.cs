using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnvilCollabResult : MonoBehaviour
{
    [SerializeField] SetCollabHolder colabHolder;
    [SerializeField] GameObject ResultHolder;
    [SerializeField] GameObject CollabEffect;
    [SerializeField] ParticleSystem Effect;
    [SerializeField] Image BuffIcon;
    [SerializeField] GameObject BuffIconHolder;
    [SerializeField] CharacterInfo_1 Character;
    [SerializeField] AudioManager Audio;

    UpgradeData data;

    private void Start()
    {
        ResultHolder.SetActive(false);
        CollabEffect.SetActive(false);
    }

    public void Set(UpgradeData upgradeData)
    {
        data = upgradeData;

        BuffIcon.sprite = data.icon;

        BuffIconHolder.SetActive(false);

        Character.AddCollab(data);

        StartCoroutine(WaitForAnimationEnd());
    }

    private IEnumerator WaitForAnimationEnd()
    {
        CollabEffect.SetActive(true);
        Audio.ClearSFX();
        Audio.PlaySFX(Audio.CollabAnvilBonk);
        Effect.Play();

        while (Effect.isPlaying)
        {
            yield return null;
        }

        CollabEffect.SetActive(false);

        SetResult();
    }

    private void SetResult()
    {
        BuffIconHolder.SetActive(true);

        ResultHolder.SetActive(true);
        colabHolder.Set(data);
        data = null;
    }
}
