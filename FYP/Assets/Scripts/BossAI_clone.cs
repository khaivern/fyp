using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAI_clone : MonoBehaviour
{

    [SerializeField] Transform target;

    [SerializeField] float speed = 200f;
    [SerializeField] float nextWaypointDistance = 3f;
    public Transform bossGFX;

    Path path;
    int currentWayPoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;

    float offsetx = 5f;
    float offsety = 3.5f;



    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);


    }

    void UpdatePath()
    {
        Vector3 destination = new Vector3(target.position.x + offsetx, target.position.y + offsety);
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, destination, OnPathComplete);
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

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if (distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if (force.x >= Mathf.Epsilon)
        {
            bossGFX.localScale = new Vector3(1f, 1f, 1f);
            offsetx = 5f;
        }
        else if (force.x <= Mathf.Epsilon)
        {
            bossGFX.localScale = new Vector3(-1f, 1f, 1f);
            offsetx = -5f;
        }
    }

    void Fire()
    {
        if (reachEndOfPath)
        {
            anim.SetTrigger("Attack");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        else
        {
            Vector2 speed = new Vector2(Random.Range(0, 2) == 1 ? -5f : 5f, Random.Range(0, 2) == 1 ? -2f : 2f);
            rb.velocity = speed;
        }

    }
}
