using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Weapon : MonoBehaviour
{
    [SerializeField] float castTimer;
    [SerializeField] float minTimeBetweenCasts = 0.2f;
    [SerializeField] float maxTimeBetweenCasts = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float projectileSpeed = 50f;

    Transform player;
    Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        castTimer = Random.Range(minTimeBetweenCasts, maxTimeBetweenCasts);
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerPos();
        CountDownToShoot();
    }

    void GetPlayerPos()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = player.position - transform.position;
        playerPos.Normalize();
    }

    void CountDownToShoot()
    {
        castTimer -= Time.deltaTime;
        if (castTimer <= 0)
        {
            Cast();
            castTimer = Random.Range(minTimeBetweenCasts, maxTimeBetweenCasts);

        }
    }

    void Cast()
    {
        GameObject laser = Instantiate(projectile, transform.position, Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = playerPos * projectileSpeed;
    }


   
}
