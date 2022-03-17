using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;
    private GameObject player;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "water" && collision.tag != "boss")
        {
            if (collision.tag == "Player")
            {
                GameObject player = GameObject.Find("Player");
                PlayerStats ps = player.GetComponent<PlayerStats>();
                ps.DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
