using UnityEngine;

public class BlueBlock : MonoBehaviour
{
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void HoverOver()
    {
        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.white);
    }

    public void HoverEnd()
    {
        rend.material.SetColor("_EmissionColor", Color.blue);
    }
}