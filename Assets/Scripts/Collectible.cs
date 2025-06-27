using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
    }

    void OnMouseDown()
    {
        GameManager.AddCollectible(gameObject.tag, 1);
        Destroy(gameObject);
    }
}
