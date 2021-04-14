using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : MonoBehaviour
{
    [Header("FireBall")]
    public GameObject fireballPrefab;
    [SerializeField] float projectileSpeed = 20f;

    [Header("Dagger Ball")]
    [SerializeField] GameObject daggerPrefab;
    [SerializeField] Transform daggerPos;
    bool summoned = false;
    Transform player;
    Vector2 playerPos;
   
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerPos = player.position - transform.position;
        playerPos.Normalize();
    }

    public void CreateFireBall()
    {
        
        GameObject laser = Instantiate(fireballPrefab, transform.position, transform.rotation);
        laser.GetComponent<Rigidbody2D>().velocity = playerPos * projectileSpeed;
    }

    public void CreateDaggerBall()
    {
        if (summoned)
        {
            return;
        }

        Instantiate(daggerPrefab, daggerPos.position, daggerPos.rotation);
        summoned = true;
    }

    public void ToggleSummon()
    {
        summoned = false;
    }

    

    
}
