using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsEnquip
{
    public ItemsData itemData;
    public GameObject itemObject;

    public ItemsEnquip(ItemsData itemData, GameObject itemObject)
    {
        this.itemData = itemData;
        this.itemObject = itemObject;
    }
}

public class PassiveItemsManager : MonoBehaviour
{
    [SerializeField] 
    Transform itemObjectTranform;

    //Danh sach ItemsPassive Prefab
    [SerializeField]
    ItemsData bloodSuckingPrefab;
    [SerializeField]
    ItemsData increaseHitSpeed;
    [SerializeField]
    ItemsData bigWeapons;
    [SerializeField]
    ItemsData TheMoreTheMerrier;
    [SerializeField]
    ItemsData TwoEdgedSword;
    [SerializeField]
    ItemsData EnergyShield;
    [SerializeField]
    ItemsData PowerOfMoneyPrefab;
    [SerializeField]
    ItemsData GlassOfKnowladgePrefab;
    [SerializeField]
    ItemsData BurningAuraPrefab;
    [SerializeField]
    ItemsData BuffAreaGainExpPrefab;
    [SerializeField]
    ItemsData ReduceCDSkillPrefab;
    [SerializeField]
    ItemsData BuffCritItemPrefab;
    [SerializeField]
    ItemsData ExchangeAtParPrefab;


    public List<ItemsEnquip> itemsEquip_lst = new List<ItemsEnquip>(5);

    int i = 0;
    float timer=0;

    /*private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0&&i==0)
        {
            i = 1000;
            AddItem(ReduceCDSkillPrefab);
        }
    }*/

    /*private void Start()
    {
        AddItem(ExchangeAtParPrefab);
    }*/


    public void AddItem(ItemsData itemData)
    {
        if(!itemsEquip_lst.Any(i => i.itemData == itemData))
        {
            itemData.level = 1;
            GameObject newItem=Instantiate(itemData.ItemBasePrefab, itemObjectTranform);
            newItem.GetComponent<ItemBase>().SetLevelBase(itemData);
            ItemsEnquip newItemsEnquip = new ItemsEnquip(itemData,newItem);
            itemsEquip_lst.Add(newItemsEnquip);
        }
        else
        {
            //do update
            ItemsEnquip data = null;

            data = itemsEquip_lst.Find(item => item.itemData == itemData);

            if(data != null)
            {
                data.itemObject.GetComponent<ItemBase>().level = itemData.level;
                data.itemObject.GetComponent<ItemBase>().SetItemStat();
            }
        }
    }

}
