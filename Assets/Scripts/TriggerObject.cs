using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    public UnityEvent onTriggerEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CallTrigger()
    {
        onTriggerEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
