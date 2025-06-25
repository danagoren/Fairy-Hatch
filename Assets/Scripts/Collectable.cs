using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        GameManager.AddCollectable(gameObject.tag, 1);
        Destroy(gameObject);
    }
}
