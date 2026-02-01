using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BugHazardSensor : MonoBehaviour
{
    public event System.Action<BugHazard> OnHazardDetected;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BugHazard hazard = other.GetComponent<BugHazard>();
        if (hazard != null) OnHazardDetected?.Invoke(hazard);
    }
}
