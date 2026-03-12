using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager.ItemType;

public class Flower : MonoBehaviour
{
    [SerializeField] private Vector2 fallVelocity = new Vector2(0, -2f);
    [SerializeField] private float outOfFrameY = -5f;
    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = fallVelocity; //fall down
    }

    private void Update()
    {
        if (transform.position.y < outOfFrameY) //when out of frame, destroy
        {
            Destroy(gameObject);
        }
    }
    private void OnMouseDown()
    {
        //add to inventory:
        if (gameObject.tag == "Flower1") GameManager.Instance.AddItem(GameManager.ItemType.Flower1, 1);
        if (gameObject.tag == "Flower2") GameManager.Instance.AddItem(GameManager.ItemType.Flower2, 1);
        if (gameObject.tag == "Flower3") GameManager.Instance.AddItem(GameManager.ItemType.Flower3, 1);
        if (gameObject.tag == "Leaf") GameManager.Instance.AddItem(GameManager.ItemType.Flower3, 1);
        if (gameObject.tag == "Branch") GameManager.Instance.AddItem(GameManager.ItemType.Flower3, 1);
        Destroy(gameObject);
    }
}
