using UnityEngine;

public class MakeSound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasBeenTriggered = false;

    void Start()
    {
        // Get the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on this object!");
        }
    }

    // Called when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the VR player (ensure your player has the "Player" tag)
        if (other.CompareTag("Hand") && !hasBeenTriggered)
        {
            audioSource.Play();
            hasBeenTriggered = true; // Prevents the sound from replaying on every frame
        }
    }

    // Optional: Reset the trigger if the player leaves the area and comes back
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            hasBeenTriggered = false;
        }
    }
}
