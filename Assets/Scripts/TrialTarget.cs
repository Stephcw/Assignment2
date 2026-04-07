using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrialTarget : MonoBehaviour
{
    private TrialRunner runner;
    private TrialData trial;
    private Renderer rend;
    void Awake()
    {
        rend = GetComponent<Renderer>();

        if (rend == null)
        {
            rend = GetComponentInChildren<Renderer>();
        }

        else
        {
            Debug.LogError("No Renderer found on target: " + gameObject.name);
        }
    }
    public void Setup(TrialRunner r, TrialData t)
    {
        runner = r;
        trial = t;
    }

    public void OnSelected(SelectEnterEventArgs args)
    {
        Debug.Log("Selected target for trial " + trial.trial);
        runner.CompleteCurrentTrial();
    }
     public void OnHoverEntered(HoverEnterEventArgs args)
    {
        rend.material.SetColor("_BaseColor", Color.green);
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        rend.material.color = Color.blue;
    }

    public void Hover(HoverEnterEventArgs args)
    {
        // optional visual feedback
        Debug.Log("Hover on trial " + trial.trial);
    }
}