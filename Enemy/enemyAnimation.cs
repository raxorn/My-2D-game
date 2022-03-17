using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimation : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 prevPos;
    private Vector2 current;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        prevPos = this.transform.position;
        current = this.transform.position;

        StartCoroutine(setDirection());
    }

    IEnumerator setDirection()
    {
        current = this.transform.position;

        animator.SetFloat("Horizontal", current.x - prevPos.x);
        animator.SetFloat("Vertical", current.y - prevPos.y);
        animator.SetFloat("Speed", 2);


        yield return new WaitForSeconds(1);
        prevPos = current;
        StartCoroutine(setDirection());
    }
}
