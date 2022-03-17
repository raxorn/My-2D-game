
using System.CodeDom;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private GameObject gamemanager;

    private int lastHorizontalMovement  = 1;
    private int lastVerticalMovement = 1;

    public float boosTime;
    //private float nextFire = 0.0F;
    private float startTime;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        PlayerStats ps = GetComponent<PlayerStats>();
        if (ps.getEnergyLeft() >= 10)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed = 10f;
                startTime = Time.time;
            }

        }

        if (startTime + boosTime < Time.time)
        {
            moveSpeed = 5f;
        }
        animation();
    }

    private void animation()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement.x == 1) lastHorizontalMovement = 1;
        if (movement.x == -1) lastHorizontalMovement = -1;
        if (movement.y == 1) lastVerticalMovement = 1;
        if (movement.y == -1) lastVerticalMovement = -1;


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }


    public int getlastHorizontalMovement()
    {
        return lastHorizontalMovement;
    }

    public int getlastVerticalMovement()
    {
        return lastVerticalMovement;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
