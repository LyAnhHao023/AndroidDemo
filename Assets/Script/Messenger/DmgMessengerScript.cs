using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgMessengerScript : MonoBehaviour
{
    [SerializeField]
    float timeLife = 0.8f;
    float timer;

    Vector3 newTranform= Vector3.zero;

    private void OnEnable()
    {
        timer = timeLife;

        newTranform = transform.position + new Vector3(Random.Range(-1,1),1,0);
    }

    private void Update()
    {
        Vector3 dic=(newTranform-transform.position).normalized;

        transform.position+=dic*3*Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            gameObject.SetActive(false);
        }
    }
}
