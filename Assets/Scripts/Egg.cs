using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    SpriteRenderer eggSprite;
    [SerializeField] Sprite WholeEgg;
    [SerializeField] Sprite HatchedEgg;

    // Start is called before the first frame update
    public void Start()
    {
        eggSprite = GetComponent<SpriteRenderer>();
        eggSprite.sprite = WholeEgg;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("NewEgg") == 1)
        {
            PlayerPrefs.SetInt("NewEgg", 0);
            eggSprite.sprite = WholeEgg;
        }
    }

    void OnMouseDown()
    {
        if (eggSprite.sprite == WholeEgg)
        {
            //if timer reached 0:
            eggSprite.sprite = HatchedEgg;
            PlayerPrefs.SetInt("EggHatched", 1);
        }
    }
}
