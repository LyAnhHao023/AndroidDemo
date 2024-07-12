using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTypeLocker : MonoBehaviour
{
    [SerializeField] GameObject locker;

    [SerializeField] MainMenu menu;

    MapData MapData;

    private void Start()
    {
        MapData = menu.GetMapData();
        SetLocker();
    }

    private void Update()
    {
        if(menu.GetMapData() != MapData)
        {
            MapData = menu.GetMapData();
            SetLocker();
        }
    }

    public void SetLocker()
    {
        locker.SetActive(!MapData.storyCleared);
        GetComponent<Button>().enabled = !MapData.storyCleared;
    }
}
