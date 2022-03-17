using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hppot : MonoBehaviour
{
    public GameObject powerUP;
    private GameObject player;
    private GameObject playerstats;

    private void Start()
    {
        playerstats = GameObject.Find("GameManager");
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        GameStats playerstat = playerstats.GetComponent<GameStats>();
        if (other.name == "Player")
        {
            player = GameObject.Find("Player");
            PlayerStats ps = player.GetComponent<PlayerStats>();
            ps.increaseHealing();
            Destroy(gameObject);
        }
    }
}
