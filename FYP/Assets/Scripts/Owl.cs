using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Owl : MonoBehaviour
{
    public AIPath aiPath;
    Animator myAnimator;

    // Attacking 
    public Transform attackPoint;
    public float attackRad = 0.5f;
    public LayerMask playerLayer;
    public Transform player;
    public float range = 1f;
    


    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = FindObjectOfType<Player>().transform;
        FlipSprite();
        WithinRange();
    }

    private void FlipSprite()
    {

        // Flip based on x speed.
        // travelling right.
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // travelling left.
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void WithinRange()
    {
        float xDiff = Mathf.Abs(player.position.x - transform.position.x);
        float yDiff = Mathf.Abs(player.position.y - transform.position.y);

        // Debug.Log("x = " + xDiff + " y = " + yDiff);
        if (xDiff <= range && yDiff <= range)
        {
            myAnimator.SetTrigger("Attacking");
        }
    }

    public void Attack()
    {

        Collider2D[] players = Physics2D.OverlapCircleAll(attackPoint.position, attackRad, playerLayer);
        foreach (Collider2D player in players)
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
