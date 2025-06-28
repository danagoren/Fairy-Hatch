using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : Collectible
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0f, -2f); //fall down
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -5) //when out of frame, destroy
        {
            Destroy(gameObject);
        }
    }
}
