using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    GameObject PointerPrefab;
    [SerializeField]
    Transform PointerPos;

    MenuManager menuManager;

    GameObject menuObject;

    GameObject PointerOJ;


    private void Start()
    {
        PointerOJ=Instantiate(PointerPrefab);
        PointerOJ.transform.parent = PointerPos.transform;
        PointerOJ.GetComponent<Window_pointer>().SetTarget(PointerPos.position);
        menuObject = GameObject.FindGameObjectWithTag("MenuManager");
        menuManager = menuObject.GetComponent<MenuManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMove player = collision.GetComponent<playerMove>();
        if (player != null)
        {
            menuManager.GetComponentInParent<MenuManager>().OpenChestScene();
            DestroyChest();
        }
    }

    public void DestroyChest()
    {
        Destroy(gameObject);
    }
}
