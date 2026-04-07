using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TargetRaySelectable : MonoBehaviour
{
    private ExperimentManager experimentManager;

    void Start()
    {
        experimentManager = FindObjectOfType<ExperimentManager>();
    }

    public void OnSelected(SelectEnterEventArgs args)
    {
        experimentManager?.TargetSelected();
    }
}