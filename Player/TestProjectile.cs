using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.name != "Player" && collision.tag != "water")
        {
            if (collision.GetComponent<Boss>() != null)
            {
                collision.GetComponent<Boss>().DealDamage(damage);
            } else if (collision.GetComponent<Enemy>() != null)
            {
                collision.GetComponent<Enemy>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
