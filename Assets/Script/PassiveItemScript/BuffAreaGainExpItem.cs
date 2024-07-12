using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAreaGainExpItem : ItemBase
{
    public override void ItemEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void SetItemStat()
    {
        throw new System.NotImplementedException();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleExp capsuleExp = collision.GetComponent<CapsuleExp>();
        if (capsuleExp != null)
        {
            capsuleExp.isFollow = true;
        }
    }

    public override void Update()
    {
        transform.localScale += new Vector3(0.00005f, 0.00005f, 0.00005f);
    }
}
