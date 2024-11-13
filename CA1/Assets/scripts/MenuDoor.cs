using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDoor : MonoBehaviour
{
    // The name of the scene to load when entering the door
    [SerializeField] private string sceneToLoad;

    void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is the player and if they are pressing "W"
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.W))
        {
            // Load the specified scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
