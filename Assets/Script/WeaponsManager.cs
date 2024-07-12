using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class weaponEnquip
{
    public WeaponData weaponData;
    public GameObject weaponObject;

    public weaponEnquip(WeaponData weaponData, GameObject weaponObject)
    {
        this.weaponData = weaponData;
        this.weaponObject = weaponObject;
    }
}

public class WeaponsManager : MonoBehaviour
{
    [SerializeField]
    Transform weaponObjectTranform;

    [SerializeField] StageEventManager stageEventManager;

    //Danh sach vu khi
    [SerializeField]
    WeaponData gunWeapon;
    [SerializeField]
    WeaponData axeWeapon;
    [SerializeField]
    WeaponData bombWeapon;
    [SerializeField]
    WeaponData ToxinZonesWeapon;
    [SerializeField]
    WeaponData ShurikenWeapon;
    [SerializeField]
    WeaponData BazokaWeapon;
    [SerializeField]
    WeaponData WallSpikeWeapon;
    [SerializeField]
    WeaponData HolyBeamPrefab;
    [SerializeField]
    WeaponData WindMagicPrefab;
    [SerializeField]
    WeaponData SuperShurikenPrefab;
    [SerializeField]
    WeaponData SuperHolyBeamPrefab;
    [SerializeField]
    WeaponData SuperWallSpikePrefab;


    public List<weaponEnquip> weapons_lst=new List<weaponEnquip>(5);

    int i = 0;
    float timer = 10;
    float time = 10;

    //private void Start()
    //{
    //    AddWeapon(BazokaWeapon);
    //}

    //private void Start()
    //{
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.First().weaponObject.GetComponent<WeaponBase>().LevelUp();

    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //    //weapons_lst.Last().weaponObject.GetComponent<WeaponBase>().LevelUp();
    //}

    public void AddWeapon(WeaponData weaponData)
    {
        if (!weapons_lst.Any(w => w.weaponData == weaponData))
        {
            GameObject weaponObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectTranform);
            weaponObject.GetComponent<WeaponBase>().SetData(weaponData);
            weaponEnquip newWeaponEnquip = new weaponEnquip(weaponData, weaponObject);
            weapons_lst.Add(newWeaponEnquip);
        }
        else
        {
            //Do update weapon

            weaponEnquip data = null;

            data = weapons_lst.Find(item => item.weaponData == weaponData);

            if (data != null)
            {
                if (!data.weaponObject.GetComponent<WeaponBase>().weaponData.maxed)
                {
                    data.weaponObject.GetComponent<WeaponBase>().LevelUp();
                    if (data.weaponObject.GetComponent<WeaponBase>().weaponData.maxed)
                    {
                        CheckCollab(data);
                    }
                }
                else
                {
                    data.weaponObject.GetComponent<WeaponBase>().OverLevelUp();
                }
            }
        }
    }

    public void RemoveWeapon(WeaponData weaponData)
    {
        weaponEnquip removeItem = null;
        for (int i = 0; i < weapons_lst.Count; i++)
        {
            if (weapons_lst[i].weaponData == weaponData)
            {
                Destroy(weapons_lst[i].weaponObject);
                removeItem = weapons_lst[i];
                break;
            }
        }

        weapons_lst.Remove(removeItem);
    }

    private void CheckCollab(weaponEnquip weapon)
    {
        weaponEnquip data = null;

        data = weapons_lst.Find(item => item.weaponObject.GetComponent<WeaponBase>().weaponStats.level == 7);

        if(data != null)
        {
            if (data.weaponData == weapon.weaponData.weaponColabData)
            {
                stageEventManager.SetColab();
            }
        }
    }
}
