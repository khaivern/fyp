using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathEffect;
    float delay = 0.4f;
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        GameObject deathEffectObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        
        Destroy(gameObject);
        Destroy(deathEffectObject, delay);
    }
    
    

}
