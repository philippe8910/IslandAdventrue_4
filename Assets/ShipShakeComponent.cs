using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShipShakeComponent : MonoBehaviour
{
    public float shakeSpeed = 1.0f; // 旋轉速度
    public float shakeAmount = 30.0f; // 旋轉角度

    private bool isShaking = false; // 判斷是否正在旋轉

    void Start()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(shakeAmount , 0 , 0)); // 初始化角度
        Shake();
    }

    void Shake()
    {
        if (isShaking) return; // 避免重複啟動

        isShaking = true;
        transform.DOLocalRotate(new Vector3(-shakeAmount, 0, 0), shakeSpeed)
            .SetLoops(-1, LoopType.Yoyo) // 循環來回
            .OnKill(() => isShaking = false); // 動畫結束後恢復狀態
    }
}
