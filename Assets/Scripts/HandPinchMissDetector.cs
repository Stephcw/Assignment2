using UnityEngine;
using UnityEngine.InputSystem;

public class HandPinchMissDetector : MonoBehaviour
{
    public TrialRunner runner;
    public InputActionReference selectAction;

    private bool hoveringTarget = false;
    private bool lastPressed = false;

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
        if (selectAction == null || selectAction.action == null || runner == null)
            return;

        bool pressed = selectAction.action.ReadValue<float>() > 0.5f;
        bool pressedThisFrame = pressed && !lastPressed;

        if (pressedThisFrame)
        {
            // If not hovering target, this pinch attempt was a miss.
            if (!hoveringTarget)
                runner.RegisterMiss();
        }

        lastPressed = pressed;
    }

    public void SetHovering(bool value)
    {
        hoveringTarget = value;
    }
}