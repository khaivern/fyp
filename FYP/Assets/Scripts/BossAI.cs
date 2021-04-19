using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BossAI : MonoBehaviour
{
    // Cached Components.
    [SerializeField] Transform player; // Player position.
    Path myPath;
    Seeker mySeeker;
    Rigidbody2D myRigidBody2D;
    Animator myAnimator;

    // Config
    [SerializeField] float speed = 200f;
    bool reachEnd = false;
    int currentWayPoint = 0;
    [SerializeField] float nextWaypointDistance = 3f; 
    [SerializeField] Transform boss;

    float offsetX = 5f;
    float offsetY = 3.5f;

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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        boss = this.gameObject.transform.GetChild(0).transform;
        mySeeker = GetComponent<Seeker>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponentInChildren<Animator>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        Vector3 destination = new Vector3(player.position.x + offsetX, player.position.y + offsetY);
        if (mySeeker.IsDone())
        {
            mySeeker.StartPath(myRigidBody2D.position, destination, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            myPath = p;
            currentWayPoint = 0; //reset progress
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
        LookAtPlayer();
    }

    private void Movement()
    {
        if (myPath == null) return;

        if (currentWayPoint >= myPath.vectorPath.Count)
        {
            reachEnd = true;
            return;
        }
        else
        {
            reachEnd = false;
        }

        Vector2 direction = ((Vector2)myPath.vectorPath[currentWayPoint] - myRigidBody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        myRigidBody2D.AddForce(force);

        float distance = Vector2.Distance(myRigidBody2D.position, myPath.vectorPath[currentWayPoint]);

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
        if (reachEnd)
        {
            myAnimator.SetTrigger("Attack");
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
            myAnimator.SetBool("Special", true);

        }
        else if (finalStage)
        {
            hasPassed = true;

            myAnimator.SetBool("Special", false);
            if (clone1 == null || clone2 == null) return;
            clone1.SetActive(true);
            clone2.SetActive(true);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Player")
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
