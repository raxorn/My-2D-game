using System.Collections;
using UnityEngine;


public class TestEnemyShooting : EnemyAttack
{
    public GameObject testEnemySpell;

    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;

    public override void Start()
    {
        base.Start();
        StartCoroutine(ShootPlayer());
            
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if (player != null)
        {
            
            Vector2 myPos = transform.position;
            Vector2 targetPos = player.transform.position;
            Vector2 direction = (targetPos - myPos).normalized;

            GameObject spell = Instantiate(testEnemySpell, transform.position, Quaternion.identity);
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            

            StartCoroutine(ShootPlayer());
        }
    }

    void Update()
    {
        
    }
}
