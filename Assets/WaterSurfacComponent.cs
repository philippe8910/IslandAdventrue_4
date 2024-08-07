using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurfacComponent : MonoBehaviour
{
    public float waveSpeed = 1.0f;
    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.SetVector("_Offset" , new Vector2(Time.time * waveSpeed , 0));
    }
}
