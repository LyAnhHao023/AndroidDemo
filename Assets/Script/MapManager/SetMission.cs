using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMission : MonoBehaviour
{
    [SerializeField] Text _missionInfo;
    [SerializeField] Text MissionProgress;

    public void Set(MissionInfo missionInfo, int killProgress = 0)
    {
        _missionInfo.text = missionInfo.decription;

        if(StaticData.MapSelect != null)
        {
            missionInfo.completed = PlayerPrefs.GetInt(missionInfo.missionName + StaticData.MapSelect.Name, 0) == 1 ? true : false;
        }

        if (missionInfo.completed)
        {
            _missionInfo.color = Color.green;
            MissionProgress.color = Color.green;
            StaticData.missionCompleteNum++;
        }

        if (missionInfo.missionType.ToString() == "HP")
            MissionProgress.text = missionInfo.completed?"Hoàn thành":"(0/1)";
        if (missionInfo.missionType.ToString() == "Kill")
            SetKillMissionProgress(missionInfo, killProgress);
    }

    public void SetHPMissionComplete(MissionInfo missionInfo, bool missionFailed = false)
    {
        if(missionFailed)
        {
            _missionInfo.color = Color.red;
            MissionProgress.color = Color.red;
            MissionProgress.text = "(0/1)";
        }
        else
        {
            _missionInfo.color = Color.green;
            MissionProgress.color = Color.green;
            MissionProgress.text = "Hoàn thành";
            missionInfo.completed = true;
            StaticData.missionCompleteNum++;
        }

        if(StaticData.MapSelect != null)
        {
            PlayerPrefs.SetInt(missionInfo.missionName + StaticData.MapSelect.Name, missionFailed ? 0 : 1);
            PlayerPrefs.Save();
            CheckFullStar();
        }
    }

    public void SetKillMissionComplete(MissionInfo missionInfo, bool missionFailed = false, int killProgress = 0)
    {
        if (missionFailed)
        {
            _missionInfo.color = Color.red;
            MissionProgress.color = Color.red;
        }
        else
        {
            _missionInfo.color = Color.green;
            MissionProgress.color = Color.green;
            missionInfo.completed = true;
            StaticData.missionCompleteNum++;
        }

        SetKillMissionProgress(missionInfo, killProgress);

        if(StaticData.MapSelect != null)
        {
            PlayerPrefs.SetInt(missionInfo.missionName + StaticData.MapSelect.Name, missionFailed ? 0 : 1);
            PlayerPrefs.Save();
            CheckFullStar();
        }
    }

    public void SetKillMissionProgress(MissionInfo missionInfo, int killProgress = 0)
    {
        MissionProgress.text = string.Format("({0}/{1})", killProgress, missionInfo.num);
    }

    void CheckFullStar()
    {
        if(StaticData.missionCompleteNum >= 3)
        {
            PlayerPrefs.SetInt("3Star" + StaticData.MapSelect.name, 1);
            PlayerPrefs.Save();
        }
    }
}
