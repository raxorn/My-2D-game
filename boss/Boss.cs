using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public int critChange;
    private bool crit;

    public Slider HealthSlider;
    public GameObject healthBar;
    protected GameObject player;
    private AudioSource enemyAudio;
    public List<AudioClip> death = new List<AudioClip>();

    public int nextLVL;

    public static bool dead;

    void Start()
    {
        player = GameObject.Find("Player");
        healthBar.SetActive(true);
        enemyAudio = GetComponent<AudioSource>();
        health = maxHealth;
        HealthSlider.value = 1;
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
        setHealthUI();
    }
    
    private bool critHit(){
        PlayerStats ps = player.GetComponent<PlayerStats>();
        critChange = ps.getCritChance();
        int rand = Random.Range(0,100 );
        if (critChange > rand){
            //AudioSource.PlayClipAtPoint(critSound, transform.position);
            return true;
        } else{
            return false;
        }
    }

    public void healCharacter(float heal)
    {
        health += heal;
        checkOverheal();
        setHealthUI();

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
            dead = true;
            StartCoroutine(Death());
        }
    }

    private void setHealthUI()
    {
        HealthSlider.value = health / maxHealth;
    }


    IEnumerator Death()
    {
        GameObject gm = GameObject.Find("GameManager");
        GameStats ps = gm.GetComponent<GameStats>();
        GetComponent<FloatToPlayer>().speed = 0.5f;
        GetComponent<Animator>().enabled = false;
        GetComponent<TestEnemyShooting>().cooldown = 10;
        
        yield return new WaitForSeconds(2);
        ps.exitGame(nextLVL);
        Destroy(gameObject);
    }

    private float healthPrecentage()
    {
        return (health / maxHealth);
    }

    public bool isBossDead()
    {
        Debug.Log("bossscriot " +dead);
        return dead;
    }
}
