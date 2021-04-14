using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLRA : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speed = 10f;
    [SerializeField] Rigidbody2D myRigidBody2D;
    [SerializeField] int damage = 100;

    Transform player;
    Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = player.position - transform.position;
        playerPos.Normalize();
        
        myRigidBody2D.velocity = playerPos * speed;
    }

    private void OnTriggerEnter2D(Collider2D otherObjectHit)
    {

        Player player = otherObjectHit.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
