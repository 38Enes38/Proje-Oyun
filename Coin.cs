using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinCollectSound;  
    private AudioSource audioSource;  

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.score += 10;

            if (coinCollectSound != null)
            {
                audioSource.PlayOneShot(coinCollectSound);
            }

            Destroy(gameObject);
        }
    }
}