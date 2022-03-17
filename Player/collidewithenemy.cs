using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidewithenemy : MonoBehaviour
{
    public float damage; 
    private GameObject playerstats;
    private GameObject player;

    private void Start()
    {
        playerstats = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        PlayerStats playerstat = player.GetComponent<PlayerStats>();

        if (coll.gameObject.tag == "boss")
        {
            playerstat.DealDamage(30);
            player.transform.position += player.transform.position - coll.gameObject.transform.position;
        }
        else if (coll.gameObject.tag == "Enemy")
        {
            playerstat.DealDamage(15);
            player.transform.position += player.transform.position - coll.gameObject.transform.position;
        }
    }
}
