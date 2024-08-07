using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CaptureObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject netPrefab;
    [SerializeField] private GameObject netEffect;

    [SerializeField] private Transform controllerTransform;
    [SerializeField] private Transform collectPointTransform;

    [SerializeField] private Vector3 netOffset;
    [SerializeField] private Vector3 controllerStartPos;

    [SerializeField] private bool isCaptured = false;

    private Vector3 previousControllerPosition;
    private float detectionThreshold = 0.05f; // 调整这个值以控制检测灵敏度

    void Start()
    {
        controllerTransform = GameObject.FindGameObjectWithTag("RightController").transform;
        collectPointTransform = GameObject.FindGameObjectWithTag("CollectPoint").transform;

        
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (!isCaptured)
            return;

        //Debug.Log("Controller position: " + controllerTransform.position);

        DetectControllerPullBack();
        previousControllerPosition = controllerTransform.position;
    }

    private void DetectControllerPullBack()
    {
        Vector3 currentControllerPosition = controllerTransform.position;
        Vector3 movement = currentControllerPosition - previousControllerPosition;

        Debug.Log("Controller movement: " + movement.z);

        // 如果控制器在Z轴方向上的移动超过了阈值，触发事件
        if (movement.z < -detectionThreshold)
        {
            OnControllerPullBack();
        }
    }

    private void OnControllerPullBack()
    {
        Debug.Log("Controller pull back");

        isCaptured = false;

        transform.DOMove(collectPointTransform.position, 1.0f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    [ContextMenu("Capture")]
    public void Capture()
    {
        var net = Instantiate(netPrefab, transform.position + Vector3.up * 20, Quaternion.identity);

        net.transform.parent = transform;

        net.transform.DOMove(transform.position + netOffset, 1.0f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            var effect = Instantiate(netEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            controllerStartPos = controllerTransform.position;
            previousControllerPosition = controllerTransform.position;

            isCaptured = true;
        });
    }
}
