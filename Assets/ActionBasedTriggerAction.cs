using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActionBasedTriggerAction : MonoBehaviour
{
    // 使用 InputActionProperty 来绑定输入操作
    [SerializeField]
    private InputActionProperty rightHandTriggerAction;

    [SerializeField] private UnityEvent OnTriggerPressed;

    private void OnEnable()
    {
        // 启用 Input Action
        rightHandTriggerAction.action.Enable();
        rightHandTriggerAction.action.performed += OnTriggerPressedBinding;
    }

    private void OnDisable()
    {
        // 禁用 Input Action
        rightHandTriggerAction.action.performed -= OnTriggerPressedBinding;
        rightHandTriggerAction.action.Disable();
    }

    private void OnTriggerPressedBinding(InputAction.CallbackContext context)
    {
        OnTriggerPressed?.Invoke();
        // 触发按键按下时执行的函数
        Debug.Log("Right Trigger pressed!");
        // 你想执行的其他操作
    }
}
