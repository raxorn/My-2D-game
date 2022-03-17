
using UnityEngine;

public class BossStats : MonoBehaviour
{
    public GameObject boss;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    public void spawn()
    {        
        Vector3 spawnPos = new Vector3();
        spawnPos.x = (int)Random.Range(0, 20) + player.transform.position.x;
        spawnPos.y = (int)Random.Range(12, 15) + player.transform.position.y;
        
        Instantiate(boss, spawnPos, Quaternion.identity);
    }
}
