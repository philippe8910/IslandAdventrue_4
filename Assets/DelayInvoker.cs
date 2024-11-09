using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class DelayInvoker : MonoBehaviour
{
    public UnityEvent events;


    async void Start()
    {
        await Task.Delay(2000);
        events.Invoke();
    }

}
