using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstBossFightEnd : MonoBehaviour
{
    [SerializeField]
    private UnityEvent fightEndEvent;

    public void checkForRemainingBosses()
    {
        if (transform.childCount == 0)
            fightEndEvent.Invoke();
    }
}
