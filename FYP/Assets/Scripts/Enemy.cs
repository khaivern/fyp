using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] GameObject deathEffect;
    float delay = 0.4f;
    [SerializeField] Vector3 offset = new Vector3(0, 2, 0);
    [SerializeField] Vector3 localScale = new Vector3(1, 1, 1);
    [SerializeField] GameObject FloatingHP;
    GameObject hp;
    [SerializeField] AudioClip deathSound;
    [SerializeField] [Range(0, 1)] float deathVol = 0.1f;
    private void Start()
    {
        DisplayHpText();
    }

    
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Die();
        }
    }

    void DisplayHpText()
    {
        
        hp = Instantiate(FloatingHP, transform.position + offset, Quaternion.identity, transform);
        InvokeRepeating("UpdateHPText", 0f, 0.1f);
    }

    void UpdateHPText()
    {
        hp.GetComponent<TextMeshPro>().text = health.ToString();
        UpdateHPPosition();
    }

    void UpdateHPPosition()
    {
        if (transform.localScale.x > 0)
        {
            hp.transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);

        }
        else
        {
            hp.transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        }
    }

    private void Die()
    {
        GameObject deathEffectObject = Instantiate(deathEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathVol);

        Destroy(gameObject);
        Destroy(deathEffectObject, delay);
    }
   
}
