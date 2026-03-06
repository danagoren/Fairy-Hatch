using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Sprite buttonOn; 
    [SerializeField] private Sprite buttonOff;
    private bool buttonState = true;
    private SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        buttonState = true;
    }

    private void Update()
    {
        buttonState = GameManager.Instance.IsEggAvailable();
        if (buttonState)
        {
            sr.sprite = buttonOn;
        }
        else
        {
            sr.sprite = buttonOff;
        }
    }

    private void OnMouseDown()
    {
        if (buttonState)
        {
            GameManager.Instance.BuyEgg();
        }
    }
}
