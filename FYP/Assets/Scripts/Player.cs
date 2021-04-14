using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25, 25);
    [SerializeField] Vector2 dmgKick = new Vector2(0, 50);
    [SerializeField] int maxHealth = 500;
    [SerializeField] int currentHealth;
    public HealthBar healthbar;
    [SerializeField] GameObject blinkPrefab;
    [SerializeField] float distance = 10000f;

    // State
    bool isAlive = true;
    bool isFacingRight = true;
    bool isInvulnerable = false;

    // Cached components
    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;
    GameSession myGameSession;

    
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody2D.gravityScale;
        healthbar = FindObjectOfType<HealthBar>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
        
    }

    // Update is called once per frame
    void Update()
    {
        healthbar = FindObjectOfType<HealthBar>();
        myGameSession = FindObjectOfType<GameSession>();

        if (!isAlive) { return; }
        Run();
        Jump();
        ClimbingLadder();
        FlipSprite();
        HazardDamage();
        SpecialSkill();
    }

    public void IncreaseMaxHealth(int maxHealth)
    {
        healthbar.SetMaxHealth(currentHealth);
        this.maxHealth = maxHealth;
        currentHealth += 500;
        
    }

    void SpecialSkill()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (myGameSession.GetCooldown())
            {
                Debug.Log(myGameSession.GetCooldown());
                return;

            }
            myGameSession.SetSkillCooldown(5f);
            
            Vector3 destination = transform.position + transform.right * distance;
            StartCoroutine(BlinkEffect());
            myRigidbody2D.AddForce(destination);

        }
    }

    IEnumerator BlinkEffect()
    {
        
        GameObject blink = Instantiate(blinkPrefab, transform.position, isFacingRight? Quaternion.identity : Quaternion.Euler(0, 180f, 0));
        yield return new WaitForSeconds(.5f);
        Destroy(blink);
    }

    private void HazardDamage()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;
        myRigidbody2D.velocity = dmgKick;
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        StartCoroutine(DamageFlicker());
        if(currentHealth <= 0)
        {
            StartCoroutine(ProcessDeathWDelay()); ;
        }
    }

    IEnumerator DamageFlicker()
    {
        isInvulnerable = true;
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
        isInvulnerable = false;
    }

    

    IEnumerator ProcessDeathWDelay()
    {
        isInvulnerable = true;
        isAlive = false;
        myAnimator.SetTrigger("Dying");
        myRigidbody2D.velocity = deathKick;
        yield return new  WaitForSeconds(2f);
        isInvulnerable = false;
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal") * runSpeed;
        Vector2 playerVelocity = new Vector2(controlThrow, myRigidbody2D.velocity.y);
        myRigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Boss")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidbody2D.velocity += jumpVelocityToAdd;
            
        }

    }

    private void FlipSprite()
    {

        /*bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1);
        }*/
        if (myRigidbody2D.velocity.x == 0) return;

        if (myRigidbody2D.velocity.x > 0 && !isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
            isFacingRight = !isFacingRight;
        }
        else if (myRigidbody2D.velocity.x < 0 && isFacingRight)
        {
            transform.Rotate(0, 180f, 0);
            isFacingRight = !isFacingRight;
        }

    }

    

    private void ClimbingLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidbody2D.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = Input.GetAxis("Vertical") * climbSpeed;
        Vector2 playerVelocity = new Vector2(myRigidbody2D.velocity.x, controlThrow);
        myRigidbody2D.velocity = playerVelocity;
        myRigidbody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);
    }

}
