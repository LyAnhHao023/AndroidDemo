using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnvilCollabResult : MonoBehaviour
{
    [SerializeField] SetCollabHolder colabHolder;
    [SerializeField] GameObject ResultHolder;
    [SerializeField] GameObject ImageAnimation;
    [SerializeField] Animator animator;
    [SerializeField] Image BuffIcon;
    [SerializeField] GameObject BuffIconHolder;
    [SerializeField] CharacterInfo_1 Character;

    UpgradeData data;

    private void Start()
    {
        ResultHolder.SetActive(false);
    }

    public void Set(UpgradeData upgradeData)
    {
        data = upgradeData;

        Character.AddCollab(data);

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
        BuffIconHolder.SetActive(true);

        BuffIcon.sprite = data.icon;

        ResultHolder.SetActive(true);
        colabHolder.Set(data);
    }
}
