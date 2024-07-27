using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTypeLocker : MonoBehaviour
{
    [SerializeField] GameObject locker;

    public void SetLocker(bool open)
    {
        locker.SetActive(!open);
        GetComponent<Button>().enabled = !open;
    }
}
