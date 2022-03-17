using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.name == "Player")
        {
            GameObject player = GameObject.Find("Player");;
            TestSpell spell = player.GetComponent<TestSpell>();
            spell.increaseNumberOfProjectiles();
            Destroy(gameObject);
        }
    }
}
