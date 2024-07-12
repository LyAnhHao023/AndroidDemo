using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashSkill1MinotairBoss : MonoBehaviour
{
    int dmg;

    GameObject character;

    public void SetTargetAndDmg(GameObject character,int dmg)
    {
        this.character = character;
        this.dmg = dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject==character.gameObject)
        {
            character.GetComponent<CharacterInfo_1>().TakeDamage(dmg);
        }
    }
}
