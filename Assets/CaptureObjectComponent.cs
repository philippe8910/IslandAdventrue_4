using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class CaptureObjectComponent : MonoBehaviour
{
    [SerializeField] private GameObject netPrefab;
    [SerializeField] private GameObject netEffect;

    [SerializeField] private Transform controllerTransform;
    [SerializeField] private Transform collectPointTransform;

    [SerializeField] private Vector3 netOffset;
    [SerializeField] private Vector3 controllerStartPos;

    [SerializeField] private GameObject effect;

    [SerializeField] private bool isCaptured = false;

    private Vector3 previousControllerPosition;
    private float detectionThreshold = 0.05f; // 调整这个值以控制检测灵敏度

    public int pullBackCount = 0;


    public bool isPull = false;

    public UnityEvent OnCaptured, OnReleased;

    private List<Vector3> posList = new List<Vector3>();

    void Start()
    {
        controllerTransform = GameObject.FindGameObjectWithTag("RightController").transform;
        collectPointTransform = GameObject.FindGameObjectWithTag("CollectPoint").transform;
    }

    float timer = 0;

    void Update()
    {
        if (!isCaptured) return;

        DetectControllerPullBack();
        previousControllerPosition = controllerTransform.position;

        if(isPull)
        {
            timer += Time.deltaTime;

            if(timer >= 1)
            {
                isPull = false;
            }
        }
    }

    private void DetectControllerPullBack()
    {
        Vector3 currentControllerPosition = controllerTransform.position;
        Vector3 movement = currentControllerPosition - previousControllerPosition;

        // 檢測是否達到拉動的門檻
        if (movement.z < -detectionThreshold)
        {
            if (pullBackCount >= 3)
            {
                
            }
            else
            {
                OnControllerPullBack();
            }
        }
    }

    private void OnControllerPullBack()
    {
        if (isPull)
            return;

        Debug.Log("Controller pull back");

        Destroy(GetComponent<FloatComponent>());
        transform.DOMove(posList[pullBackCount], 1.0f).SetEase(Ease.InOutQuart).onComplete += delegate { 
            if(pullBackCount >= 3)
            {
                OnReleased.Invoke();
                Destroy(Instantiate(effect, transform.position, transform.rotation), 1);
                Destroy(gameObject);
            }
        };
        isPull = true;
        pullBackCount++;
    }

    [ContextMenu("Test")]
    public void Capture()
    {
        if (TreasureSpawnPoint.Instance.isCapture)
            return;

        var net = Instantiate(netPrefab, transform.position + Vector3.up * 20, Quaternion.identity);
        net.transform.parent = transform;

        // 計算三次拉動的目標位置，距離逐步減少
        posList.Add(transform.position + (collectPointTransform.position - transform.position) / 3 * 1);
        posList.Add(transform.position + (collectPointTransform.position - transform.position) / 3 * 2);
        posList.Add(collectPointTransform.position);

        net.transform.DOMove(transform.position + netOffset, 1.0f).SetEase(Ease.InOutQuart).OnComplete(() =>
        {
            var effect = Instantiate(netEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);

            controllerStartPos = controllerTransform.position;
            previousControllerPosition = controllerTransform.position;

            isCaptured = true;
        });

        OnCaptured.Invoke();
    }
}
