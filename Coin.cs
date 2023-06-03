using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinCollectSound;  // Ses dosyas�
    private AudioSource audioSource;  // Ses kayna��

    private void Start()
    {
        // Ses kayna��n� al
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.score += 10;
            audioSource.PlayOneShot(coinCollectSound);  // Ses �al
            Destroy(gameObject);  // Alt�n nesnesini yok et
        }
    }
}
