using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickUp : MonoBehaviour
{
    [SerializeField] AudioClip diamondPickUpSFX;
    [SerializeField] float volume = 0.5f;
    [SerializeField] int points = 100;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<GameSession>().AddToScore(points);
        AudioSource.PlayClipAtPoint(diamondPickUpSFX, Camera.main.transform.position, volume);
        Destroy(gameObject);
    }
}
