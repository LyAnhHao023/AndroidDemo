using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public List<GoodsData> goodsDatas;

    public GameObject goodsPerfab;
    public GameObject goodsTranform;

    GameObject goodsHolder;

    private void Start()
    {
        for (int i = 0; i < goodsDatas.Count; i++)
        {
            goodsHolder = Instantiate(goodsPerfab, goodsTranform.transform);

            //if ((int)goodsDatas[i].type == 0)
                goodsDatas[i].level = PlayerPrefs.GetInt(goodsDatas[i].goodsName + "lv", 0);

            /*if ((int)goodsDatas[i].type == 0)
            {
                PlayerPrefs.SetInt(goodsDatas[i].goodsName + "lv", 0);
                PlayerPrefs.Save();
            }*/


            if ((int)goodsDatas[i].type == 1 && goodsDatas[i].level == 1)
            {
                goodsDatas[i].characterData.acquired = true;
                goodsDatas[i].characterData.skillTree.type = PlayerPrefs.GetInt("TreeType" + goodsDatas[i].characterData.name, 0);
                goodsDatas[i].characterData.skillTree.level = PlayerPrefs.GetInt("TreeLevel" + goodsDatas[i].characterData.name, 0);
            }

            goodsHolder.GetComponent<SetItemShop>().Set(goodsDatas[i]);
        }
    }
}
