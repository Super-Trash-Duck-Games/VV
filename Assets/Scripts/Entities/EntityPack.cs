using UnityEngine;
using System.Collections.Generic;

public class EntityPack : MonoBehaviour
{

    void Start()
    {
        List<Transform> children = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i));
        }

        foreach (Transform child in children)
        {
            child.parent = null;
        }
    }
}
