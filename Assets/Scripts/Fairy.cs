using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager.ItemType;

public class Fairy : MonoBehaviour
{
    [SerializeField] private Sprite[] fairySprites;
    private SpriteRenderer sr;
    private float animTimePeriod = 0.2f;
    private float animTimePassed = 0;
    private int animCount = 0;
    private bool animForward = true;
    private Vector3[] fairyPositions = new Vector3[]
    {
        new Vector3 (-0.15f, 0.3f, 0),
        new Vector3 (-0.25f, 0.55f, 0),
        new Vector3 (-0.3f, 0.4f, 0)
    };

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = fairySprites[0];
        animForward = true;
        gameObject.transform.position = fairyPositions[GetFairyIndex()];
    }

    private void Update()
    {
        Anim();
    }

    private void OnMouseDown() //bug: sometimes this doesnt get called 
    {
        if (GetFairyIndex() == 0) GameManager.Instance.AddItem(GameManager.ItemType.Fairy1, 1);
        if (GetFairyIndex() == 1) GameManager.Instance.AddItem(GameManager.ItemType.Fairy2, 1);
        if (GetFairyIndex() == 2) GameManager.Instance.AddItem(GameManager.ItemType.Fairy3, 1);
        GameManager.Instance.CollectEgg();
        Destroy(gameObject); 
    }

    private int GetFairyIndex()
    {
        if (gameObject.tag == "Fairy1") return 0;
        if (gameObject.tag == "Fairy2") return 1;
        if (gameObject.tag == "Fairy3") return 2;
        return -1;
    }

    private void Anim()
    {
        animTimePassed += Time.deltaTime;
        if (animTimePassed >= animTimePeriod)
        {
            animTimePassed = 0;
            sr.sprite = fairySprites[animCount];
            if (animCount == fairySprites.Length - 1) animForward = false; //if reached end of frames, change direction
            if (animCount == 0) animForward = true; //if reached beginning of frames, change direction
            animCount += animForward ? 1 : -1; //next frame
        }
    }
}
