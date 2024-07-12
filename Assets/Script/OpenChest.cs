using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator animator;

    public GameObject chest;

    public GameObject buffHolder;

    public GameObject openButton;
    public GameObject getBuffButton;
    public GameObject ignoreBuffButton;

    [SerializeField] LevelUpSelectBuff selectBuff;
    [SerializeField] CharacterInfo_1 characterInfo;
    [SerializeField] UpgradeButton upgradeButton;
    [SerializeField] MenuManager menuManager;

    List<UpgradeData> upgradeDatas;

    List<WeightedItem> items = new List<WeightedItem>
    {
        new WeightedItem(3, 5f),
        new WeightedItem(2, 10f),
        new WeightedItem(1, 85f),
    };

    int currentIndex = -1;

    private void Awake()
    {
        animator = chest.GetComponent<Animator>();
        buffHolder.transform.LeanScale(Vector2.zero, 0f);
        openButton.SetActive(true);
        getBuffButton.SetActive(false);
        ignoreBuffButton.SetActive(false);
    }

    public int GetRandomType(List<WeightedItem> items)
    {
        float totalWeight = items.Sum<WeightedItem>(item => item.weight);
        float randomValue = Random.Range(0, totalWeight);

        float weights = 0f;
        foreach (var item in items)
        {
            weights += item.weight;
            if (randomValue <= weights)
            {
                return item.type;
            }
        }

        return -1;
    }

    private IEnumerator WaitForAnimationEnd()
    {
        animator.SetBool("Open", true);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("OpenChestAnimation"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        Set();
    }

    public void OpenC()
    {
        upgradeDatas = selectBuff.GetUpgrades(GetRandomType(items));
        characterInfo.upgradeDatasFromChest = upgradeDatas;

        currentIndex = -1;

        StartCoroutine(WaitForAnimationEnd());
    }

    public void ChestStand()
    {
        menuManager.CloseChestScene();

        animator.SetBool("Open", false);

        buffHolder.transform.LeanScale(Vector2.zero, 0.5f).setIgnoreTimeScale(true);
    }

    public void Set()
    {
        currentIndex++;

        openButton.SetActive(false);
        getBuffButton.SetActive(true);
        ignoreBuffButton.SetActive(true);

        upgradeButton.Set(upgradeDatas[currentIndex]);

        buffHolder.transform.LeanScale(Vector2.one, 0.25f).setIgnoreTimeScale(true);
    }

    public void Choice(bool isGet)
    {
        if (isGet)
        {
            characterInfo.UpgradeFromChest(currentIndex);
        }

        buffHolder.transform.LeanScale(Vector2.zero, 0f).setIgnoreTimeScale(true);

        if (currentIndex < upgradeDatas.Count - 1)
        {
            Set();
        }
        else
        {
            ChestStand();
        }
    }
}
