using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MapData mapData;

    /*private void Awake()
    {
        PlayerPrefs.SetInt("Knight Girllv", 1);
        PlayerPrefs.Save();
    }*/

    public void SetMapData(MapData data)
    {
        mapData = data;
    }

    public MapData GetMapData()
    { 
        return mapData;
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGame(int id)
    {
        StaticData.LevelType = id;
        SceneManager.LoadScene(mapData.name);
    }
}
