using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class RestartGame : MonoBehaviour
{
    private InputDevice targetDevice;
    private bool isAButtonPressed = false;


    void Start()
    {
        // 獲取當前可用的控制器
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevices(inputDevices);

        foreach (var device in inputDevices)
        {
            // 假設你使用的是手柄或控制器設備，檢查是否支持按鈕
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller))
            {
                targetDevice = device;
                break;
            }
        }
    }

    void Update()
    {
        if (targetDevice.isValid)
        {
            // 檢查 A 鍵（通常對應於 `primaryButton`）
            bool aButtonPressed;
            if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonPressed) && aButtonPressed)
            {
                if (!isAButtonPressed)
                {
                    // 觸發按鈕按下事件
                    OnAButtonPressed();
                    isAButtonPressed = true;
                }
            }
            else
            {
                isAButtonPressed = false;
            }
        }
    }

    // 當 A 鍵被按下時觸發的事件
    private void OnAButtonPressed()
    {
        SceneManager.LoadScene("Story");
        Debug.Log("A button was pressed!");
        // 在此處可以處理按鈕按下後的邏輯
    }
}
