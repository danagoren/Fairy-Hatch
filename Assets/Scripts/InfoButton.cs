using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoButton : MonoBehaviour
{
    private bool state = false;
    private void OnMouseDown()
    {
        if (state)
        {
            GameManager.Instance.InfoOverlay(false);
            state = false;
        }
        else
        {
            GameManager.Instance.InfoOverlay(true);
            state = true;
        }
    }
}
