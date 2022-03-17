using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestSpell : MonoBehaviour{


    public List<GameObject> projectilesList = new List<GameObject>();
    private GameObject projectile;
    private int numberOfProjectiles = 1;
    public float projectileForce;
    public float minDamage;
    public float maxDamage;

    private GameObject gamemanager;
    private float counter;
    private int currentProjectileLVL;
    public int lvl;

    private AudioSource shootingAudio;
    public float fireRate;
    private float nextFire = 0.0F;
    private float startTime;
    public float boosTime;


    private void Start()
    {
        currentProjectileLVL = 0;
        projectile = projectilesList[currentProjectileLVL];
        numberOfProjectiles = 1;
        if (lvl > 1)
        {
            loadPlayerData();
        }
        
    }

    void Update()
    {
        shootingAudio = GetComponent<AudioSource>();

        if (Input.GetMouseButton(0))
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                shootingAudio.Play();
                shoot();
            }                     
        }
        GameObject player = GameObject.Find("Player");
        PlayerStats ps = player.GetComponent<PlayerStats>();
        if (ps.getEnergyLeft() >= 10)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                fireRate = 0.1f;
                startTime = Time.time;
            }
        }
        if (startTime + boosTime < Time.time)
        {
            fireRate = 0.2f;
        }
    }

    private void shoot()
    {
        for (int i = 0; i < getNumberOfProjectiles(); i++)
        {            
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = getTargetPos(mousePos, i);

            Vector2 myPos = (Vector2)transform.position;
            Vector2 direction = (mousePos - myPos).normalized;

            double angle = Math.Atan2(direction.y, direction.x) * 180 / Math.PI;
            Vector3 dir = new Vector3(0,0, (int)angle - 180);

            GameObject spell = Instantiate(projectile, myPos , Quaternion.identity);

            spell.transform.rotation = Quaternion.Euler(dir);
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestProjectile>().damage = UnityEngine.Random.Range(minDamage, maxDamage);
        }
    }
          
    private Vector2 getTargetPos(Vector2 pos, int i)
    {
        if (i % 2 == 0) // 2,4,6 ect
        {
            if (i == 0)
            {
                counter = 0;
            }
            else
            {
                pos.x -= getCounter();
                pos.y -= getCounter();
            }
            increaseCounter();
        }
        else // 1, 3, 5, 7 ect
        {
            pos.x += getCounter();
            pos.y += getCounter();
        }
        return pos;
    }

    private void increaseCounter()
    {
        counter += 0.5f;
    }

    private float getCounter()
    {
        return counter;
    }

    public int getNumberOfProjectiles()
    {
        return numberOfProjectiles;
    }

    public void increaseNumberOfProjectiles()
    {
        numberOfProjectiles++;

    }

    public void upgradeProjectile()
    {
        currentProjectileLVL++;
        minDamage += 5;
        maxDamage += 5;        
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("numProj", numberOfProjectiles);
        PlayerPrefs.SetFloat("min", minDamage);
        PlayerPrefs.SetFloat("max", maxDamage);
        PlayerPrefs.SetInt("lvl", currentProjectileLVL);

        Debug.Log("save projectile numbers" + PlayerPrefs.GetInt("numProj"));
        Debug.Log("save min dmg" + PlayerPrefs.GetFloat("min"));
        Debug.Log("save max dmg" + PlayerPrefs.GetFloat("max"));
        Debug.Log("save projectilelvl" + PlayerPrefs.GetInt("lvl"));
    }

    private void loadPlayerData()
    {        
        numberOfProjectiles = PlayerPrefs.GetInt("numProj");

        minDamage = PlayerPrefs.GetFloat("min");
        maxDamage = PlayerPrefs.GetFloat("max");
        currentProjectileLVL = PlayerPrefs.GetInt("lvl");
        projectile = projectilesList[currentProjectileLVL];

        /*
        Debug.Log("numProj " + numberOfProjectiles);
        Debug.Log("mindmg " + minDamage);
        Debug.Log("max dmg " + maxDamage);
        Debug.Log("currentProjectileLVL " + currentProjectileLVL);
        */
    }
}
