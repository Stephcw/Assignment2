using UnityEngine;
using UnityEngine.InputSystem;


public class RayMissDetector : MonoBehaviour
{
    public TrialRunner runner;
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor; // works with Near-Far Interactor
    public InputActionReference selectAction;

    private bool lastPressed = false;

    void Start()
    {
        if (runner == null)
            runner = FindObjectOfType<TrialRunner>();

        if (interactor == null)
            interactor = GetComponentInChildren<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();
    }

    void OnEnable()
    {
        if (selectAction != null && selectAction.action != null)
            selectAction.action.Enable();
    }

    void OnDisable()
    {
        if (selectAction != null && selectAction.action != null)
            selectAction.action.Disable();
    }

    void Update()
    {
        if (runner == null || interactor == null || selectAction == null)
            return;

        bool pressed = selectAction.action.ReadValue<float>() > 0.5f;
        bool pressedThisFrame = pressed && !lastPressed;

        if (pressedThisFrame)
        {
            // Check if hovering a valid target
            var interactable = interactor.firstInteractableSelected;

            if (interactable == null)
            {
                runner.RegisterMiss();
            }
            else
            {
                TrialTarget target = interactable.transform.GetComponent<TrialTarget>();
                if (target == null)
                {
                    runner.RegisterMiss();
                }
                // else → hit handled by OnSelected()
            }
        }

        lastPressed = pressed;
    }
}