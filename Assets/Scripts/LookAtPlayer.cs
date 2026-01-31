using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform objectToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        if (objectToLookAt == null) {
            objectToLookAt = GameObject.FindWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(objectToLookAt.position);
    }
}
