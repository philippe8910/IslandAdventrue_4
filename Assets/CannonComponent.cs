using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CannonComponent : MonoBehaviour
{
    [SerializeField] private Transform controller;
    [SerializeField] private float lerpSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.DORotate(new Vector3(transform.rotation.eulerAngles.x, controller.eulerAngles.y, transform.rotation.eulerAngles.z), lerpSpeed);
    }
}
