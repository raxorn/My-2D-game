using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public int critChange;
    private bool crit;

    private GameObject player;
    public GameObject healthBar;
    public Slider healthBarSlider;    
    public List<GameObject> powerUpDrop = new List<GameObject>();
    private int antallpowerupdrops;
    private AudioSource enemyAudio;
    public List<AudioClip> death = new List<AudioClip>();
    public AudioClip critSound;

    void Start()
    {   
        antallpowerupdrops = powerUpDrop.Count;
        player = GameObject.Find("Player");
        enemyAudio = GetComponent<AudioSource>();
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        if(critHit()){
            crit = true; 
            damage *= 2;
        } else{
            crit = false;
        }

        healthBar.SetActive(true);
        health -= damage;

        Vector2 myPos = transform.position;
        EnemyDamageText.Create(myPos,(int)damage, crit);
        checkDeath();
        healthBarSlider.value = healthPrecentage();
    }

    private bool critHit(){
        PlayerStats ps = player.GetComponent<PlayerStats>();
        critChange = ps.getCritChance();
        int rand = Random.Range(0,100 );
        if (critChange > rand){
            AudioSource.PlayClipAtPoint(critSound, transform.position);
            return true;
        } else{
            return false;
        }
    }

    public void healCharacter(float heal)
    {
        health += heal;
        checkOverheal();

        healthBarSlider.value = healthPrecentage();
    }

    private void checkOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void checkDeath()
    {   
        if (health <= 0)
        {
            int rand = Random.Range(0, death.Count);

            AudioSource.PlayClipAtPoint(death[rand], transform.position);
            increaseKillCount();
            dropLoot();
            Destroy(gameObject);
        }
    }

    private void increaseKillCount()
    {
        GameObject gm = GameObject.Find("LevelManager");
        levelStats ls = gm.GetComponent<levelStats>();
        ls.mobKilled();
    }

    private int getDropRate()
    {
        return 15;
    }

    private void dropLoot()
    {
        int droprate = getDropRate();

        int rand = Random.Range(0, 100);

        if (droprate >= rand){
            int randDrop = Random.Range(0, antallpowerupdrops);
            Instantiate(powerUpDrop[randDrop], transform.position, Quaternion.identity);
        }
    }

    private float healthPrecentage()
    {
        return (health / maxHealth);
    }
}
