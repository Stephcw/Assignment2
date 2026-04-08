using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrialTarget : MonoBehaviour
{
    private TrialRunner runner;
    private TrialData trial;
    private Renderer rend;
    private Color originalColor = Color.white;

    private HandPinchMissDetector pinchDetector;

    void Awake()
    {
        rend = GetComponent<Renderer>();

        if (rend == null)
            rend = GetComponentInChildren<Renderer>();

        if (rend == null)
        {
            Debug.LogError("No Renderer found on target: " + gameObject.name);
        }
        else
        {
            originalColor = rend.material.color;
        }

        pinchDetector = FindObjectOfType<HandPinchMissDetector>();
    }

    public void Setup(TrialRunner r, TrialData t)
    {
        runner = r;
        trial = t;
    }

    public void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("Selected target for trial " + trial.trial);
        runner.RegisterHit();
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        if (rend != null)
        {
            rend.material.color = Color.green;
            rend.material.SetColor("_BaseColor", Color.green);
        }

        if (pinchDetector != null)
            pinchDetector.SetHovering(true);
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        if (rend != null)
        {
            rend.material.color = originalColor;
            rend.material.SetColor("_BaseColor", originalColor);
        }

        if (pinchDetector != null)
            pinchDetector.SetHovering(false);
    }
}