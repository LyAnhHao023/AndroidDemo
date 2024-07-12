using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindButtonWithTag : MonoBehaviour
{
    [SerializeField] GameObject children;
    [SerializeField] GameObject collab;

    public GameObject AcceptButton()
    {
        return children;
    }

    public GameObject CollabButton()
    {
        return collab;
    }
}
