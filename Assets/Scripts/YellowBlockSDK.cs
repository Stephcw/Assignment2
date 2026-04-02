using UnityEngine;

public class YellowBlockSDK : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("YELLOW ENTER: " + other.name);

        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("YELLOW EXIT: " + other.name);

        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}