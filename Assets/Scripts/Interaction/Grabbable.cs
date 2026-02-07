using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    public bool IsGrabbed { get; private set; }

    public void SetGrabbed(bool b)
    {
        IsGrabbed = b;
        if (IsGrabbed)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}
