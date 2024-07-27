using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatExplosion : MonoBehaviour
{
    [SerializeField]
    BombBatScipt BombBat;

    private void OnEnable()
    {
        BombBat.DestroyBombBat();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 player= collision.GetComponent<CharacterInfo_1>();
        if(player != null)
        {
            BombBat.ApllyDmgToPlayer();
        }
    }


}
