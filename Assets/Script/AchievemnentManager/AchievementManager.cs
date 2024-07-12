using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] GameObject AchievementPanel;

    public List<AchievementData> achievementDatas;

    public GameObject achievementPefab;
    public GameObject achievementTranform;

    GameObject achievementHolder;

    public int totalPoints;

    private void Start()
    {
        for (int i = 0; i < achievementDatas.Count; i++)
        {
            achievementHolder = Instantiate(achievementPefab, achievementTranform.transform);

            achievementDatas[i].complete = PlayerPrefs.GetInt(achievementDatas[i].achieName, -1) == 1 ? true : false;

            achievementHolder.GetComponent<SetAchievement>().Set(achievementDatas[i]);

            if (achievementDatas[i].complete)
            {
                totalPoints += achievementDatas[i].points;

                PlayerPrefs.SetInt("TotalPoints", totalPoints);
                PlayerPrefs.Save();
            }
        }
    }
}
