using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetMapHolder : MonoBehaviour
{
    [SerializeField] Button MapHolder;
    [SerializeField] Text Name;
    [SerializeField] GameObject lockHolder;

    MainMenu menu;

    private MapManager mapManager;
    private GameObject LevelSelect;
    private GameObject SelectCharacter;
    private Button endless;
    private Button challange;

    MapData mapData;

    int numComplete = 0;

    private void Awake()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        menu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();

        LevelSelect = mapManager.LevelSelectPanel();
        SelectCharacter = mapManager.CharacterSelect();
        endless = mapManager.Endless();
        challange = mapManager.Challange();


        MapHolder.onClick.AddListener(Onclick);
    }

    public void Set(MapData data)
    {
        MapHolder.image.sprite = data.Icon;
        Name.text = data.Name.ToUpper();
        mapData = data;
        lockHolder.SetActive(!data.unlocked);
        MapHolder.enabled = data.unlocked;

        foreach(var item in data.missions)
        {
            int completed = PlayerPrefs.GetInt(item.missionName + data.Name, 0);
            item.completed = completed > 0;
            if(completed > 0)
                numComplete++;

            if (numComplete >= 3)
            {
                PlayerPrefs.SetInt("3Star" + data.key, 1);
            }
        }
    }

    void Onclick()
    {
        LevelSelect.SetActive(false);
        SelectCharacter.SetActive(true);
        menu.SetMapData(mapData);
        StaticData.MapSelect = mapData;

        if (mapData.storyCleared)
            endless.enabled = true;
        if (numComplete >= 3)
            challange.enabled = true;
    }
}
