using UnityEngine;

public class PizzaPortal : MonoBehaviour
{
    // Reference to the AudioSource component
    private AudioSource audioSource;

    // Audio clips for default and collision music
    public AudioClip mainSong;
    public AudioClip collisionSong;

    // Flag to check if collision song is currently playing
    private bool isCollisionSongPlaying = false;

    void Start()
    {
        // Get the AudioSource component attached to the GameObject
        audioSource = GetComponent<AudioSource>();

        // Ensure the audio source is valid and assign the main song
        if (audioSource != null && mainSong != null)
        {
            audioSource.clip = mainSong;
            audioSource.loop = true;  // Loop the main song
            audioSource.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if we are already playing the collision song, or if collisionSong is not assigned
        if (isCollisionSongPlaying || collisionSong == null)
            return;

        // Change to the collision song
        audioSource.Stop();
        audioSource.clip = collisionSong;
        audioSource.loop = false;  // Optional: play collision song only once
        audioSource.Play();

        isCollisionSongPlaying = true;
    }
}
