using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationAngleInteraction : XRSimpleInteractable
{
    public Transform center; // 中心點
    public Transform start; // 起點
    public Transform end; // 終點

    public Transform trackingController; // 選擇的互動器

    public bool isRight; // 是否往右邊旋轉
    public bool isHovered = false; // 是否被 Hover

    public float rotateAngle; // 角度

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);

        start.position = new Vector3(start.position.x, args.interactor.transform.position.y, args.interactor.transform.position.z);
        trackingController = args.interactor.transform;
        isHovered = true;
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        base.OnHoverExited(args);

        start.localPosition = Vector3.zero;
        end.localPosition = Vector3.zero;
        trackingController = null;
        isHovered = false;
    }


    void Update()
    {
        if (isHovered)
        {
            end.position = new Vector3(end.position.x, trackingController.position.y, trackingController.position.z);
            DetectingRotate();

            transform.Rotate(new Vector3(rotateAngle, 0, 0));
        }
    }


    public void DetectingRotate()
    {
        if (center != null && start != null && end != null)
        {
            // 計算起點和終點相對於中心點的向量
            Vector3 startVector = start.position - center.position;
            Vector3 endVector = end.position - center.position;

            // 確保向量不為零
            if (startVector != Vector3.zero && endVector != Vector3.zero)
            {
                // 計算兩個向量之間的角度
                float angle = Vector3.SignedAngle(startVector, endVector, Vector3.right);

                // 使用 Vector3.Cross 來判斷方向
                Vector3 crossProduct = Vector3.Cross(startVector, endVector);
                string direction = crossProduct.y > 0 ? "左邊" : "右邊";

                isRight = crossProduct.y > 0;

                // 顯示向量和角度
                Debug.Log("Start Vector: " + startVector);
                Debug.Log("End Vector: " + endVector);
                Debug.Log("Signed Angle: " + angle);
                Debug.Log("Direction: " + direction);

                rotateAngle = angle;

                // 判斷角度的方向並輸出結果
                if (angle > 0)
                {
                    Debug.Log("起點到終點旋轉了 " + angle + " 度，往右邊");
                }
                else if (angle < 0)
                {
                    Debug.Log("起點到終點旋轉了 " + Mathf.Abs(angle) + " 度，往左邊");
                }
                else
                {
                    Debug.Log("起點到終點沒有旋轉");
                }
            }
            else
            {
                Debug.LogWarning("起點或終點向量為零，無法計算角度");
            }
        }
        else
        {
            Debug.LogWarning("需要設置 center、start 和 end 變數");
        }
    }

    
}
