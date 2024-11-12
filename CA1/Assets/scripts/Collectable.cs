using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    
    // Sound to play when coin is collected
    public AudioClip collectSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object colliding is the player
        if (other.CompareTag("Player"))
        {
            // Increase the score
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.IncreaseScore(1); // Increase score by 1
            }

            // Play the collection sound at the collectible's position
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, 500000000000);
            }

            // Destroy the collectible immediately after playing the sound
            Destroy(gameObject);
        }
    }
}
