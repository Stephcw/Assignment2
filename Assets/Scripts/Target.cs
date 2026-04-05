using UnityEngine;

public class Target : MonoBehaviour
{
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = Color.blue;
    }

    public void Hover()
    {
        rend.material.color = Color.green;
    }

    public void ResetColor()
    {
        rend.material.color = Color.blue;
    }
}
