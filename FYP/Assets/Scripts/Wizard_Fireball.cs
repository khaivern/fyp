using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Fireball : MonoBehaviour
{
    public int damage = 50;
    [SerializeField] GameObject expEffect;
    [SerializeField] AudioClip impactSound;
    [SerializeField] [Range(0,1)] float impactVol = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(impactSound, Camera.main.transform.position, impactVol);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D otherObjectHit)
    {
        Player player = otherObjectHit.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }

        
        Destroy(gameObject);

        GameObject expEffectObject = Instantiate(expEffect, transform.position, Quaternion.identity);
        Destroy(expEffectObject, 1.005f);

    }
}
