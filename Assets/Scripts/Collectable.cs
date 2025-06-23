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
        if (gameObject.tag == "gift")
        {
            Collect("Flowers1", 5);
            Collect("Flowers2", 5);
            Collect("Flowers3", 5);
        }
        else 
        {
            Collect(gameObject.tag, 1);
        }
        Destroy(gameObject);
    }

    void Collect(string str, int num){
        GameManager.AddCollectable(str, num);
    }
}
