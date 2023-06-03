using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinCollectSound;  // Ses dosyasý
    private AudioSource audioSource;  // Ses kaynaðý

    private void Start()
    {
        // Ses kaynaðýný al
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            player.score += 10;
            audioSource.PlayOneShot(coinCollectSound);  // Ses çal
            Destroy(gameObject);  // Altýn nesnesini yok et
        }
    }
}
