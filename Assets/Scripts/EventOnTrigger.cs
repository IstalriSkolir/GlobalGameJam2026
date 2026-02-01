using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnTrigger : MonoBehaviour
{

    public UnityEvent m_MyEvent;
    public bool destroyOnEnter = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_MyEvent.Invoke();
            if (destroyOnEnter)
            {
                Destroy(gameObject);
            }
        }
    }
}
