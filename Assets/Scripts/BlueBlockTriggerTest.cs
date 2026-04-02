using UnityEngine;

public class BlueBlockTriggerTest : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.blue;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("BLUE ENTER: " + other.name);
        rend.material.color = Color.white;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("BLUE EXIT: " + other.name);
        rend.material.color = Color.blue;
    }
}