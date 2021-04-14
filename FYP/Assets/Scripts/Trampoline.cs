using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] Vector2 speed = new Vector2(0, 100);
    [SerializeField] AudioClip trampSound;
    [SerializeField] [Range(0,1)] float trampVol = 0.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = speed;
            AudioSource.PlayClipAtPoint(trampSound, Camera.main.transform.position, trampVol);

        }
    }
}
