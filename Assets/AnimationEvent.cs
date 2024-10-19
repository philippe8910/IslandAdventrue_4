using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject nextScene;

    public void Next()
    {
        gameObject.SetActive(true);
        nextScene.SetActive(true);
    }
}
