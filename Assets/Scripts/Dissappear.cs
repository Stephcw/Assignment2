using UnityEngine;

public class Dissappear : MonoBehaviour
{

    void Start()
    {
    }

    // Called when another collider enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the VR player (ensure your player has the "Player" tag)
        if (other.CompareTag("Hand"))
        {
            gameObject.SetActive(false);
        }
    }

    // Optional: Reset the trigger if the player leaves the area and comes back
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            gameObject.SetActive(true);
        }
    }
}
