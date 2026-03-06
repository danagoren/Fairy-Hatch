using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    [SerializeField] private Sprite WholeEgg;
    [SerializeField] private Sprite HatchedEgg;
    private SpriteRenderer sr;

    public void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    public void Init()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = WholeEgg;
        gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        if (sr.sprite == WholeEgg)//&&if timer reached 0
        {
            sr.sprite = HatchedEgg;
            GameManager.Instance.SpawnFairy();
        }
    }
}
