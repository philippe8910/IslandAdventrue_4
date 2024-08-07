using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WheelControlComponent : MonoBehaviour
{
    [SerializeField] private Transform rockRoot;
    [SerializeField] private Vector3 defaultRotation;
    [SerializeField] private float rotationWeighted = 10;
    // Start is called before the first frame update
    void Start()
    {
        defaultRotation = transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log((360 - transform.rotation.eulerAngles.x) / rotationWeighted);

        rockRoot.DORotateQuaternion(Quaternion.Euler(new Vector3(0 , ((360 - transform.rotation.eulerAngles.y) / rotationWeighted) , 0)) , 15f
        ).SetEase(Ease.OutQuart);
    }
}
