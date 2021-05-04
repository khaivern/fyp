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
    bool isReadyToShoot = true;
    [SerializeField] AudioClip shotSound;
    [SerializeField] [Range(0, 1)] float shotVol = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<GameSession>().GetPressed()) return;
     
        if (Input.GetButtonDown("Fire1") && isReadyToShoot)
        {
            StartCoroutine(PreventSpam());
            firingCoroutine = StartCoroutine(Shoot());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator PreventSpam()
    {
        isReadyToShoot = false;
        yield return new WaitForSeconds(0.2f);
        isReadyToShoot = true;
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, shotVol);
            yield return new WaitForSeconds(fireRate);
        }
        
    }
    
}
