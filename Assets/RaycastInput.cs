using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastInput : MonoBehaviour
{
    public LineRenderer line;

    public Transform start, end;

    public GameObject go;

    public InputActionProperty inputActionProperty;
    // Start is called before the first frame update
    void Start()
    {
        inputActionProperty.action.Enable();
        inputActionProperty.action.performed += OnTriggerPressedBinding;
    }

    private void OnDisable()
    {
        // 禁用 Input Action
        inputActionProperty.action.performed -= OnTriggerPressedBinding;
        inputActionProperty.action.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, start.position);
        line.SetPosition(1, end.position);

        RaycastHit hit; // 存储射线碰撞信息
                        // 发射射线
        if (Physics.Raycast(start.position, start.forward, out hit, 100f))
        {
            // 如果射线碰撞到物体
            Debug.Log("Hit: " + hit.collider.name); // 输出碰撞的物体名称
            go = hit.transform.gameObject;
        }
        else
        {
            go = null;
        }
    }

    private void OnTriggerPressedBinding(InputAction.CallbackContext context)
    {
        if(go != null && go.GetComponent<CaptureObjectComponent>())
        {
            go.GetComponent<CaptureObjectComponent>().Capture();

        
        }
    }
}
