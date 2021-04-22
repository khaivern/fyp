using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D myRigidBody2D;
    [SerializeField] int damage = 50;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator myAnimator;
    GameSession myGameSession;
    bool hitEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D.velocity = transform.right * speed ;
        myGameSession = FindObjectOfType<GameSession>();
    }
  
    public int GetDamage()
    {
        if (FindObjectOfType<GameSession>().GetDD())
        {
            damage = 100;
            return damage;
        }
        else
        {
            damage = 50;
            return damage;
        }
        
    }


    private void OnTriggerEnter2D(Collider2D otherObjectHit)
    {
        
        Enemy enemy = otherObjectHit.GetComponent<Enemy>();
        if(enemy != null && !hitEnemy)
        {
            hitEnemy = true;
            enemy.TakeDamage(damage);
        }

        Boss boss = otherObjectHit.GetComponent<Boss>();
        if (boss != null && !hitEnemy)
        {
            hitEnemy = true;
            boss.TakeDamage(damage);
        }

        myAnimator.SetTrigger("Impact");
        myRigidBody2D.velocity = Vector2.zero;
        Destroy(gameObject, 0.3f);
    }
}
