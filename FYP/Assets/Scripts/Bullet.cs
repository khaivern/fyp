using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] Rigidbody2D myRigidBody2D;
    [SerializeField] int damage = 100;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
        myRigidBody2D.velocity = transform.right * speed ;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D otherObjectHit)
    {
        
        Enemy enemy = otherObjectHit.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Boss boss = otherObjectHit.GetComponent<Boss>();
        if (boss != null)
        {
            boss.TakeDamage(damage);
        }

        myAnimator.SetTrigger("Impact");
        Destroy(gameObject);
    }
}
