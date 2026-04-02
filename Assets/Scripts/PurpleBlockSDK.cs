using UnityEngine;

public class PurpleBlockSDK : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("PURPLE ENTER: " + other.name);
        if (rend != null)
        {
            rend.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("PURPLE EXIT: " + other.name);
        if (rend != null)
        {
            rend.enabled = true;
        }
    }
}