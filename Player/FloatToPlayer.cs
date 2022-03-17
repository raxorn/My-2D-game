using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatToPlayer : MonoBehaviour
{
    public float speed;

    private GameObject player ;
    
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {
        if(player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }       
    }
}
