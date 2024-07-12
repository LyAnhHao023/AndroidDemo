using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllChildren : MonoBehaviour
{
    public List<GameObject> children;

    public List<GameObject> characterHolder()
    {
        return children;
    }

    public void GetData()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>(true);

        foreach (Transform child in allChildren)
        {
            if (child.CompareTag("MapButton"))
            {
                children.Add(child.gameObject);
            }
        }
    }
}
