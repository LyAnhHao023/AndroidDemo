using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField]
    float timeLife=0.13f;

    private void Awake()
    {
        Destroy(gameObject,timeLife);
    }
}
