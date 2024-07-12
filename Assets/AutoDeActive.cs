using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeActive : MonoBehaviour
{
    [SerializeField] float timeDeActive;
    void OnEnable()
    {
        StartCoroutine(DeActive());
    }

    private IEnumerator DeActive()
    {
        yield return new WaitForSeconds(timeDeActive);
        gameObject.SetActive(false);
    }
}
