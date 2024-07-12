using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObject
{
    public MissionInfo missionInfo;
    public GameObject missionHolder;

    public MissionObject(MissionInfo missionInfo, GameObject missionHolder)
    {
        this.missionInfo = missionInfo;
        this.missionHolder = missionHolder;
    }
}

public class MissionManager : MonoBehaviour
{
    public List<MissionInfo> missions;

    public List<MissionObject> missionsObject = new List<MissionObject>(3);

    public GameObject MissionPerfab;
    public GameObject MissionTranform;

    GameObject missionHolder;


    private void Awake()
    {
        if(StaticData.MapSelect != null)
            missions = StaticData.MapSelect.missions;
    }

    private void Start()
    {
        if(missions != null)
        {
            foreach(var mission in missions)
            {
                missionHolder = GameObject.Instantiate(MissionPerfab, MissionTranform.transform);
                missionHolder.GetComponent<SetMission>().Set(mission);
                MissionObject missionObject = new MissionObject(mission, missionHolder);
                missionsObject.Add(missionObject);
            }
        }
    }
}
