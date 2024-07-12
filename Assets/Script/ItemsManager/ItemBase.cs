using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    public ItemsData itemsData;
    public int level;

    public void SetLevelBase(ItemsData itemBase)
    {
        itemsData = itemBase;
        level = itemBase.level;
    }

    public virtual void Update()
    {
    }

    public abstract void SetItemStat();

    public abstract void ItemEffect();
}
