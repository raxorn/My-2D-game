using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    public float spawnRate;
    private GameObject player;
    public int mapsize;

    private Collider2D[] colliders;
    private bool canSpawnHere = true;
    private float y, x;
    private Vector3 spawnPos;

    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(SpawnTestEnemy());
    }

    IEnumerator SpawnTestEnemy()
    {
        PlayerMovement playermovement = player.GetComponent<PlayerMovement>();
        x = (int)Random.Range(8, 15) * playermovement.getlastHorizontalMovement() + player.transform.position.x;
        y = (int)Random.Range(8, 15) * playermovement.getlastVerticalMovement() + player.transform.position.y;
 
        spawnPos.x = x;
        spawnPos.y = y;
        canSpawnHere = preventSpawnOverlap();


        if (canSpawnHere)
        {
            var newEnemy = Instantiate(Enemies[0], spawnPos, Quaternion.identity);
            newEnemy.transform.parent = this.transform;
        }

        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnTestEnemy());
    }

    private int getnewX(int x)
    {
        if (x < 2) return x + 20;
        else return x - 20;
    }

    private int getnewY(int y)
    {
        if (y < 2) return y + 20;
        else return y - 20;

    }

    private bool preventSpawnOverlap()
    {
        colliders = Physics2D.OverlapCircleAll(spawnPos, 3);
        for (int i = 0; i < colliders.Length; i++)
        {
            Vector3 centerPoint = colliders[i].bounds.center;
            float width = colliders[i].bounds.extents.x;
            float height = colliders[i].bounds.extents.y;

            float leftExtent = centerPoint.x - width;
            float rightExtent = centerPoint.x + width;
            float lowerExtent = centerPoint.y - height;
            float upperExtent = centerPoint.y + height;

            if (spawnPos.x >= leftExtent && spawnPos.x <= rightExtent)
            {
                if(spawnPos.y >= lowerExtent && spawnPos.y <= upperExtent)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
