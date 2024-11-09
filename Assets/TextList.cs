using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TextList : MonoBehaviour
{
    public Text t;

    public List<string> stringList; // 字符串列表
    public UnityEvent onEndOfList; // 到达列表末尾时调用的事件

    public int currentIndex = 0; // 当前字符串索引

    public InputActionProperty playerControls;

    public AudioClip[] audioClips;
    public AudioSource audioSource;

    private void OnEnable()
    {
        playerControls.action.Enable();
        playerControls.action.performed += OnTriggerPressedBinding;
    }

    private void OnDisable()
    {
        playerControls.action.Disable();
    }

    private void OnTriggerPressedBinding(InputAction.CallbackContext context)
    {
        // 检查当前索引是否在有效范围内
        if (currentIndex < stringList.Count)
        {
            currentIndex++; // 切换到下一个字符串
            t.text = stringList[currentIndex];

            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();

            Debug.Log("Current String: " + stringList[currentIndex]);
        }
        else
        {
            onEndOfList.Invoke(); // 调用 UnityEvent
            Debug.Log("Reached the end of the list.");
        }
    }

    [ContextMenu("test")]
    public void Test()
    {
        // 检查当前索引是否在有效范围内
        if (currentIndex < stringList.Count)
        {
            currentIndex++; // 切换到下一个字符串
            t.text = stringList[currentIndex];

            audioSource.clip = audioClips[currentIndex];
            audioSource.Play();

            Debug.Log("Current String: " + stringList[currentIndex]);
        }
        else
        {
            onEndOfList.Invoke(); // 调用 UnityEvent
            Debug.Log("Reached the end of the list.");
        }
    }

    private void SwitchString()
    {
        // 检查当前索引是否在有效范围内
        if (currentIndex < stringList.Count - 1)
        {
            currentIndex++; // 切换到下一个字符串
        }
        else
        {
            onEndOfList.Invoke(); // 调用 UnityEvent
        }
    }

    private void Start()
    {
        // 显示第一个字符串
        if (stringList.Count > 0)
        {
            Debug.Log("Current String: " + stringList[currentIndex]);
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }
}
