using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class critPowerUp : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            GameObject player = GameObject.Find("Player");
            PlayerStats ps = player.GetComponent<PlayerStats>();
            ps.increaseCritChance();
            Destroy(gameObject);
        }
    }
}
