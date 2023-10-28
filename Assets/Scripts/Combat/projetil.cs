using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projetil : bulletBase
{
    public int damage;

    void Start()
    {
        damage = 1;

        if (bulletSpeed == 0)
        {
            bulletSpeed = 100;
        }

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse);
        if (gameObject != null)
        {
            Destroy(gameObject, 1);
        }
    }

    void onTriggerEnter2D (Collider2D other)
    {
        // if(other.CompareTag("enemy"))
        // {
        //     enemyController enemy = other.GetComponent<enemyController>();
        //     if(enemy != null)
        //     {
        //         enemy.takeDamage(damage);
        //     }

        // }
    }

    void onCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "destructible" && gameObject != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
