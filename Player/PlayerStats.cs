using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private AudioSource thisaudio;
    public AudioClip playerDamageTaken;
    public float maxHealth;
    public float maxEnergy;

    private float health;
    private float energy;
    private int critchance;
    private int healing = 1;
    public int lvl;


    private healthBar hsBar;
    private energyBar enBar;

    private GameObject gamemanager;

    // Start is called before the first frame update
    void Start()
    {

        health = maxHealth;
        energy = maxEnergy;
        critchance = 0;
        
        //Debug.Log("neste" + nextLvl + " bossdead " + isBossDead());

        hsBar = GetComponent<healthBar>();
        enBar = GetComponent<energyBar>();

        hsBar.setHealthUI(health, maxHealth);
        enBar.setEnergyUI(energy, maxEnergy);
        if (lvl > 1)
        {
            loadPlayerData();
        }


        InvokeRepeating("healCharacter", 2.0f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && energy > 0)
        {
            energyUsed();
        }
        
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        AudioSource.PlayClipAtPoint(playerDamageTaken, transform.position);
        checkDeath();
        hsBar.setHealthUI(health, maxHealth);
    }

    private void healCharacter()
    {
        health += healing;
        checkOverheal();
        hsBar.setHealthUI(health, maxHealth);
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
            gamemanager = GameObject.Find("GameManager");
            GameStats gs = gamemanager.GetComponent<GameStats>();
            gs.exitGame(0);
        }
    }

    public void energyUsed()
    {
        energy -= 10;
        enBar.setEnergyUI(energy, maxEnergy);
    }

    public float getEnergyLeft()
    {
        return energy;
    }

    public int getCritChance(){
        return critchance;
    }
    public void increaseCritChance(){
        critchance += 2;
    }

    public void increaseHealing(){
        healing += 1;
    }

    public void saveData(){
        PlayerPrefs.SetInt("critchance", critchance);
        PlayerPrefs.SetInt("healing", healing);
    }

    private void loadPlayerData()
    {
        critchance = PlayerPrefs.GetInt("critchance");
        healing = PlayerPrefs.GetInt("healing");
    }
}

