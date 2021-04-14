using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D myRigidbody2D;
    BoxCollider2D myBoxCollider2D;
    [SerializeField] GameObject enrageVFXPrefab;

    [SerializeField] Transform player;
    [SerializeField] bool isFlipped = false;
    [SerializeField] int maxHealth = 5000;
    [SerializeField] int currentHealth;
    [SerializeField] HealthBar healthbar;

    public bool isInvulnerable = false;
    private bool isEnraged = false;

    [Header("Death")]
    float delay = 0.4f;
    [SerializeField] GameObject deathEffect;

    [Header("Load Next Level")]
    [SerializeField] GameObject portalPrefab;
    Vector2 spawnPoint = new Vector2(-36.45f, -1.37f);

    Animator animator;
    // Enrage Stage
    // Floating 
    [Header("Floating")]
    [SerializeField] float floatSpeed;
    [SerializeField] Vector2 floatDirection;
    // Top Bottom Attack
    [Header("TopBottomAtk")]
    [SerializeField] float attackSpeed;
    [SerializeField] Vector2 attackDirection;
    private int contactDamage = 50;
    // Direct Attack
    [Header("DirectAttack")]
    [SerializeField] float directSpeed;
    Vector2 playerPos;
    private bool hasPlayerPos;

    // Other
    [Header("Other")]
    [SerializeField] Transform groundCheckUp;
    [SerializeField] Transform groundCheckDown;
    [SerializeField] Transform groundCheckSide;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingSide;
    private bool isGoingUp = true;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);

        floatDirection.Normalize();
        attackDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingSide = Physics2D.OverlapCircle(groundCheckSide.position, groundCheckRadius, groundLayer);
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isEnraged)
        {
            return;
        }
        Player player = other.GetComponent<Player>();
        if (player != null)
        {

            player.TakeDamage(contactDamage);
        }
    }

    public void RandomAttack()
    {
        int randomState = Random.Range(0, 3);
        if(randomState == 0)
        {
            animator.SetTrigger("TBAttack");
        } 
        else if(randomState == 1)
        {
            animator.SetTrigger("DirAttack");
        }
        else if (randomState == 2)
        {
            animator.SetTrigger("LRA_Attack");
        }
    }

    public void floatState()
    {
        if (isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        } 
        else if (isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }

        if (isTouchingSide)
        {
            if (isFlipped)
            {
                SideDirection();
            }
            else if (!isFlipped)
            {
                SideDirection();
            }
        }

        myRigidbody2D.velocity = floatSpeed * floatDirection;
    }


    public void TopBottomAttack()
    {
        if (isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }

        if (isTouchingSide)
        {
            if (isFlipped)
            {
                SideDirection();
            }
            else if (!isFlipped)
            {
                SideDirection();
            }
        }

        myRigidbody2D.velocity = attackSpeed * floatDirection;
    }

    public void DirectAttack()
    {
        if (!hasPlayerPos)
        {
            playerPos = player.position - transform.position;
            playerPos.Normalize();
            hasPlayerPos = true;
        }
        if (hasPlayerPos)
        {
            myRigidbody2D.velocity = playerPos * attackSpeed;
        }

        // Touching sides or wall OR floor or ground
        if (isTouchingDown || isTouchingSide)
        {
            myRigidbody2D.velocity = Vector2.zero;
            hasPlayerPos = false;
            animator.SetTrigger("Slam");
        }
    }

    void ChangeDirection()
    {
        isGoingUp = !isGoingUp;
        floatDirection.y *= -1;
        attackDirection.y *= -1;
    }

    void SideDirection()
    {
        floatDirection.x *= -1;
        attackDirection.x *= -1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(groundCheckUp.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckDown.position, groundCheckRadius);
        Gizmos.DrawWireSphere(groundCheckSide.position, groundCheckRadius);
    }

    public void SetMovementSpeed()
    {
        myRigidbody2D.velocity = Vector2.zero;
    }

    public void FlipSprite()
    {

        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180f, 0);
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0, 180f, 0);
            isFlipped = false;

        }

    }

    public void TakeDamage(int damage)
    {

        // invulnerable part
        if (isInvulnerable)
        {
            return;
        }

        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 1500)
        {
            animator.SetBool("isEnraged", true);

        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject deathEffectObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Instantiate(portalPrefab, spawnPoint, Quaternion.identity);
        Destroy(gameObject);
        Destroy(deathEffectObject, delay);
    }

    // Enrage intro animations 
    public void EnrageAnim()
    {
        isInvulnerable = true;
        isEnraged = true;
        GameObject obj = Instantiate(enrageVFXPrefab, transform.position, transform.rotation);
        Destroy(obj, 7f);
    }

    public void DisplayWing()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        isInvulnerable = false;
    }

    // Freezes enemy from being pushed by player
    /*private void OnCollisionStay2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            myRigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionExit2D(Collision2D player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            myRigidbody2D.constraints = RigidbodyConstraints2D.None;
        }
    }*/
}
