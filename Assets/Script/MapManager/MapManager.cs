using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject characterSelect;
    [SerializeField] SetTypeLocker endlessSet;
    [SerializeField] SetTypeLocker challangeSet;
    [SerializeField] Button endless;
    [SerializeField] Button challange;

    public List<MapData> mapDatas;

    public GameObject mapHolderPerfab;
    public GameObject MapHolderTranform;

    public FindAllChildren findAllChildren;

    GameObject mapHolder;

    private void Start()
    {
        for (int i = 0; i < mapDatas.Count; i++)
        {
            bool stageClear = PlayerPrefs.GetInt(mapDatas[i].key, 0) == 1 ? true : false;
            mapDatas[i].storyCleared = stageClear;
            if(stageClear && i - 1 < mapDatas.Count)
            {
                mapDatas[i + 1].unlocked = stageClear;
            }


            mapHolder = Instantiate(mapHolderPerfab, MapHolderTranform.transform);
            mapHolder.GetComponent<SetMapHolder>().Set(mapDatas[i]);
        }

        findAllChildren.GetData();
    }

    public GameObject LevelSelectPanel()
    {
        return levelSelectPanel;
    }

    public GameObject CharacterSelect()
    {
        return characterSelect;
    }

    public Button Endless()
    {
        return endless;
    }

    public Button Challange()
    {
        return challange;
    }

    public SetTypeLocker EndlessSet()
    {
        return endlessSet;
    }

    public SetTypeLocker ChallangeSet()
    {
        return challangeSet;
    }
}
