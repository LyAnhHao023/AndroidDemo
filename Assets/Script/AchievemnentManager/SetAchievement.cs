using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAchievement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Text name;
    [SerializeField] Text description;
    [SerializeField] Text points;
    [SerializeField] GameObject Locker;

    public void Set(AchievementData achievementData)
    {
        if(achievementData.type.ToString() == "Count")
        {
            int total = PlayerPrefs.GetInt(achievementData.key, 0);
            achievementData.complete = total >= achievementData.num;
        }
        else
        {
            achievementData.complete = PlayerPrefs.GetInt(achievementData.key, 0) == 1 ? true : false;
        }

        icon.sprite = achievementData.icon;
        name.text = achievementData.achieName;
        description.text = achievementData.description;
        points.text = string.Format("+ {0}", achievementData.points);
        Locker.SetActive(!achievementData.complete);
    }
}
