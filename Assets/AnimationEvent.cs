using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour
{
    public GameObject nextScene;

    public UnityEvent unity;

    public void Next()
    {
        unity.Invoke();
    }
}
