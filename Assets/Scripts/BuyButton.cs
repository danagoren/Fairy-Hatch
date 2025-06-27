using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] Sprite ButtonOn;
    [SerializeField] Sprite ButtonOff;

    SpriteRenderer SR;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isEggAvailable)
        {
            SR.sprite = ButtonOn;
        }
        else
        {
            SR.sprite = ButtonOff;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.isEggAvailable)
        {
            PlayerPrefs.SetInt("PendingEggFlag", 1);
        } 
    }
}
