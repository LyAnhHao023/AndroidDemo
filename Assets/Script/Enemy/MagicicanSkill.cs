using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicicanSkill : MonoBehaviour
{
    int dmg;
    GameObject targetGameObject;

    private void OnEnable()
    {
        Invoke("DeActive", 0.4f);
    }

    public void SetDmg(int Dmg,GameObject gameObject)
    {
        dmg = Dmg;
        targetGameObject = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == targetGameObject)
        {
            targetGameObject.GetComponent<CharacterInfo_1>().TakeDamage(dmg * 2);
        }
    }

    private void DeActive()
    {
        gameObject.SetActive(false);
    }
}
