using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class BugBaitSensor : MonoBehaviour
{
    public event System.Action<BugBait> OnBaitDetected;
    public event System.Action<BugBait> OnBaitLost;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        BugBait bait = other.GetComponent<BugBait>();
        if (bait != null) OnBaitDetected?.Invoke(bait);
    }

    private void OnTriggerExit(Collider other)
    {
        BugBait bait = other.GetComponent<BugBait>();
        if (bait != null) OnBaitLost?.Invoke(bait);
    }
}
