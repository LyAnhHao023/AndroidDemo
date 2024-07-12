using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicicanAttackScript : MonoBehaviour
{
    int dmgSkill = 2;
    [SerializeField]
    float timeAutoDestroy = 10f;
    public void SetDmg(int dmg)
    {
        dmgSkill = dmg;
    }

    private void Awake()
    {
        Destroy(gameObject, timeAutoDestroy);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 player = collision.gameObject.GetComponent<CharacterInfo_1>();
        if (player != null)
        {
            player.TakeDamage(dmgSkill);
            Destroy(gameObject);
        }
    }
}
