using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOutImage : MonoBehaviour
{
    [SerializeField] GameObject imageHolder;

    private void Update()
    {
        imageHolder.transform.localScale -= new Vector3(0.00003f, 0.00003f, 0.00003f);
    }
}
