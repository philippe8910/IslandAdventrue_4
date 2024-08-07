using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxRotationComponent : MonoBehaviour
{
    public float rotationSpeed = 1.0f;
    public Material skyboxMaterial;

    // Update is called once per frame
    void Update()
    {
        skyboxMaterial.SetFloat("_Rotation", Time.time * rotationSpeed);
    }
}
