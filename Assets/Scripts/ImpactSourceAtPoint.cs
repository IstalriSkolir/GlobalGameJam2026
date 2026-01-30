using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ImpactSourceAtPoint : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    // Start is called before the first frame update
    void TriggerImpulse(float impact)
    {
        impulseSource.GenerateImpulse(impact);
    }
}
