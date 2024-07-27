using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetSelectedItem : MonoBehaviour
{
    [SerializeField] Button SelectedButton;
    [SerializeField] Image Icon;
    [SerializeField] GameObject IconHolder;
    [SerializeField] GameObject AcceptButtonOverlay;
    [SerializeField] SetCollabAcceptButton setCollab;

    public UpgradeData data = null;
    private GameObject itemOverlay;

    public UpgradeData GetData()
    {
        return this.data;
    }

    private void Start()
    {
        SelectedButton.onClick.AddListener(Onclick);
    }

    public void Clear()
    {
        data = null;
        setCollab.Clear();
        Set(data);
    }

    public void Set(UpgradeData upgradeData, GameObject overlay = null)
    {
        data = upgradeData;

        itemOverlay = overlay;

        if (data == null)
        {
            Icon.sprite = null;
            IconHolder.SetActive(false);
        }
        else
        {
            Icon.sprite = upgradeData.icon;
            IconHolder.SetActive(true);
        }

        if(data != null)
            setCollab.SetData(data);
    }

    void Onclick()
    {
        setCollab.RemoveData(data);
        data = null;
        itemOverlay.SetActive(false);
        Set(data);
        AcceptButtonOverlay.SetActive(true);
    }
}
