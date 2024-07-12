using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessengerSystem : MonoBehaviour
{
    public static MessengerSystem instance;

    [SerializeField]
    GameObject dmgMessengerPrefab;

    List<TMPro.TextMeshPro> messengerList=new List<TMPro.TextMeshPro>(10);

    int objectCout = 20;
    int count = 0;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < objectCout; i++)
        {
            CreateMessengerObject();
        }
    }

    private void CreateMessengerObject()
    {
        GameObject newMesObject = Instantiate(dmgMessengerPrefab, transform);
        messengerList.Add(newMesObject.GetComponent<TMPro.TextMeshPro>());
        newMesObject.SetActive(false);
    }

    public void Heal(Vector3 pos,int heal)
    {
        messengerList[count].gameObject.SetActive(true);
        messengerList[count].transform.position = pos;
        messengerList[count].text = "+" + heal;
        messengerList[count].color = new Color32(0, 255, 0, 255);
        count++;
        if (count >= objectCout)
        {
            count = 0;
        }
    }

    public void Miss(Vector3 pos)
    {
        messengerList[count].gameObject.SetActive(true);
        messengerList[count].transform.position = pos;
        messengerList[count].text = "miss";
        messengerList[count].color = new Color32(173, 216, 230, 255);
        count++;
        if (count >= objectCout)
        {
            count = 0;
        }
    }

    public void DmgPopUp(string dmg, Vector3 worldPosition,bool isCrit)
    {
        
        if(isCrit)
        {
            messengerList[count].gameObject.SetActive(true);
            messengerList[count].transform.position = worldPosition;
            messengerList[count].text = dmg;
            messengerList[count].color = new Color32(255, 0, 0, 255);
            count++;
        }
        else
        {
            messengerList[count].gameObject.SetActive(true);
            messengerList[count].transform.position = worldPosition;
            messengerList[count].text = dmg;
            messengerList[count].color = new Color32(255, 255, 255, 255);
            count++;
        }
        if(count >= objectCout)
        {
            count = 0;
        }
    }
}
