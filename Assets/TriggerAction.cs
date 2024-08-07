using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TriggerAction : MonoBehaviour
{
    public XRController handController;
    public InputHelpers.Button triggerButton = InputHelpers.Button.Trigger;
    public float activationThreshold = 0.1f;

    public UnityEvent OnTriggerPressed;

    void Start()
    {
        if (!handController)
        {
            handController = GetComponent<XRController>();
        }
    }

    void Update()
    {
        if (handController)
        {
            bool isPressed;
            handController.inputDevice.IsPressed(triggerButton, out isPressed, activationThreshold);

            if (isPressed)
            {
                OnTriggerPressed?.Invoke();
            }
        }
    }
}
