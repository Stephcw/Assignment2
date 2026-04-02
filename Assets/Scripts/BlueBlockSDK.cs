using UnityEngine;

public class BlueBlockSDK : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void HoverOver()
    {
        if (rend == null) return;

        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.white);
    }

    public void HoverEnd()
    {
        if (rend == null) return;

        rend.material.EnableKeyword("_EMISSION");
        rend.material.SetColor("_EmissionColor", Color.blue);
    }
}