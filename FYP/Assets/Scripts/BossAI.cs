using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAI : MonoBehaviour
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

    // Timer and stage variables.
    bool secondStage = false;
    bool finalStage = false;
    public GameObject myTimer;
    public GameObject myPrefab;
    public GameObject clone1;
    public GameObject clone2;
    public GameObject successScreen;
    bool hasPassed = false;

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
        if (!secondStage)
        {
            Movement();
            Fire();
        }
        StageManager();

        StageChecker();

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

   

    void StageChecker()
    {
        float timeLeft = myTimer.GetComponent<Timer>().GetTime();
            // Debug.Log("time left " + timeLeft + " hasPassed bool = " + hasPassed);

        if (timeLeft <= 0 && hasPassed)
        {
            Time.timeScale = 0f;
            successScreen.SetActive(true);

            return;
        }
        if (15f < timeLeft && timeLeft <= 30f)
        {
            secondStage = true;
        }
        else if (timeLeft <= 15f)
        {
            secondStage = false;
            finalStage = true;
            
        }
    }

    void StageManager()
    {
        if (secondStage)
        {
            anim.SetBool("Special", true);

        }
        else if (finalStage)
        {
            hasPassed = true;

            anim.SetBool("Special", false);
            if (clone1 == null || clone2 == null) return;
            clone1.SetActive(true);
            clone2.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
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
