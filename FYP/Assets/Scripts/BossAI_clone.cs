using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAI_clone : MonoBehaviour
{

    [SerializeField] Transform player;

    [SerializeField] float speed = 200f;
    [SerializeField] float nextWaypointDistance = 3f;
    [SerializeField] Transform boss;

    Path path;
    int currentWayPoint = 0;
    bool reachEndOfPath = false;

    Seeker mySeeker;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;

    float offsetX = 5f;
    float offsety = 3.5f;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = this.gameObject.transform.GetChild(0).transform;
        mySeeker = GetComponent<Seeker>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);


    }

    void UpdatePath()
    {
        Vector3 destination = new Vector3(player.position.x + offsetX, player.position.y + offsety);
        if (mySeeker.IsDone())
        {
            mySeeker.StartPath(myRigidBody2D.position, destination, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Fire();
        LookAtPlayer();
    }

    private void Movement()
    {
        if (path == null) return;

        if (currentWayPoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;

            return;
        }
        else
        {
            reachEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - myRigidBody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        myRigidBody2D.AddForce(force);

        float distance = Vector2.Distance(myRigidBody2D.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

    }

    void LookAtPlayer()
    {
        if (boss.transform.position.x > player.position.x) boss.localScale = new Vector2(-1f, 1f);
        else boss.localScale = new Vector2(1f, 1f);

        if (myRigidBody2D.velocity.x >= Mathf.Epsilon) offsetX = 5f;
        else offsetX = -5f;
    }

    void Fire()
    {
        if (reachEndOfPath)
        {
            myAnimator.SetTrigger("Attack");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            myRigidBody2D.velocity = new Vector2(0f, 0f);
        }
        else
        {
            Vector2 speed = new Vector2(Random.Range(0, 2) == 1 ? -5f : 5f, Random.Range(0, 2) == 1 ? -2f : 2f);
            myRigidBody2D.velocity = speed;
        }

    }
}
