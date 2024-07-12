using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterAttacked : MonoBehaviour
{
    [SerializeField] float disableTime=0.8f;
    float timer;

    [SerializeField] Transform ScaleParent;

    float scaleCur=1;

    ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        timer = disableTime;
    }

    private void LateUpdate()
    {
        if (ScaleParent.localScale.x > scaleCur)
        {
            scaleCur=ScaleParent.localScale.x;
            particleSystem.startSize += 5 * (scaleCur - 1);
        }

        timer-= Time.deltaTime;
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponentInParent<AxeWeapon>().ApllyDmg(collision);
    }
}
