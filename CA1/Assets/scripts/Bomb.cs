using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object has the tag "bomb"
        if (collision.gameObject.CompareTag("bombWall"))
        {
            // Destroy this game object to make it disappear
            Destroy(gameObject);
        }
    }

}

