using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatComponent : MonoBehaviour
{
    public float floatAmplitude = 1.0f; // 浮動的振幅
    public float floatFrequency = 1.0f; // 浮動的頻率
    public float floatSpeed = 1.0f; // 浮動的速度

    private Vector3 startPos;

    void Start()
    {
        // 記錄物體的初始位置
        startPos = transform.position;
    }

    void Update()
    {
        // 計算浮動的偏移量
        float offset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        // 計算新的位置
        Vector3 newPos = startPos;
        newPos.y += offset * floatSpeed;

        // 更新物體的位置
        transform.position = newPos;
    }
}
