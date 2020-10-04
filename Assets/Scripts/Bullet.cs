using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Player") && !other.tag.Equals("Collectable") && !other.tag.Equals("Untagged"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Equals("Wall"))
        {
            // ToDo: Dust would be nice lol 
            Destroy(gameObject);
        }
    }
}
