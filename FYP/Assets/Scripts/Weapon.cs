using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Config
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    Coroutine firingCoroutine;
    float fireRate = 0.3f;
    
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameSession>().GetPressed()) return;
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(Shoot());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(fireRate);
        }
        
    }
    
}
