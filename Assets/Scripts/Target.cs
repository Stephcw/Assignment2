using UnityEngine;
using System.Collections;

public class SimpleSphereBehavior : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Call this when the sphere is selected
    public void Activate()
    {
        StartCoroutine(ChangeAndDisappear());
    }

    IEnumerator ChangeAndDisappear()
    {
        // Change color to green
        rend.material.color = Color.green;

        // Wait for 1 second (you can change this)
        yield return new WaitForSeconds(1f);

        // Destroy the sphere
        Destroy(gameObject);
    }
}
