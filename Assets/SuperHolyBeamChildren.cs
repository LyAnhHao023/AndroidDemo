using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperHolyBeamChildren : MonoBehaviour
{
    [SerializeField] float disableTime = 0.5f;
    float timer;

    private void OnEnable()
    {
        timer = disableTime;
    }

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<SuperHolyBeamWeapon>().ApllyDmg(collision);
    }
}
