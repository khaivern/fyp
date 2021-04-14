using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float detectionRange = 3.5f;
    Rigidbody2D myRigidBody2D;
    BoxCollider2D myFeet2D;
    Transform player;
    Animator myAnimator;

    // Attacking 
    
    public Transform attackPoint;
    public float attackRad = 0.5f;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myFeet2D = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        WithinRange();
    }

    bool IsFacingRight()
    {
        return transform.localScale.x > 0f;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody2D.velocity.x)), 1f);
    }

    private void WithinRange()
    {
        bool xDist = Mathf.Abs(transform.position.x - player.position.x) <= detectionRange;
        bool yDist = Mathf.Abs(transform.position.y - player.position.y) <= detectionRange;

        if (xDist && yDist)
        {

            if (Vector2.Distance(player.position, myRigidBody2D.position) <= (detectionRange - 2.6f))
            {
                myAnimator.SetTrigger("Attacking");
                myRigidBody2D.velocity = Vector2.zero;
            } else
            {
                ChasePlayer();
            }
            
        }
        else
        {
            if (IsFacingRight())
            {
                myRigidBody2D.velocity = new Vector2(moveSpeed, 0f);
            }
            else
            {
                myRigidBody2D.velocity = new Vector2(-moveSpeed, 0f);
            }
        }
    }

    private void ChasePlayer()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) < 0.2f)
        {

            myRigidBody2D.velocity = new Vector2(0, 0);
            return;
        }

        if (transform.position.x < player.position.x)
        {
            transform.localScale = new Vector2(1, 1f);
            myRigidBody2D.velocity = new Vector2(2f, 0);
        }
        else
        {
            transform.localScale = new Vector2(-1, 1f);
            myRigidBody2D.velocity = new Vector2(-2f, 0);
        }
    }

    public void Attack()
    {
        
        Collider2D[] players =  Physics2D.OverlapCircleAll(attackPoint.position, attackRad, playerLayer);
        foreach(Collider2D player in players)
        {
            player.GetComponent<Player>().TakeDamage(30);
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRad);
    }
}
