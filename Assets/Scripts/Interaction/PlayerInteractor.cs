using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObjectDetectorZone detectorZone;
    [SerializeField] private Transform holdAnchorPoint;
    private Grabbable heldGrabbable;

    private void Awake()
    {
        //detectedGrabbables = new List<Grabbable>();
        //detectorZone.OnObjectEntered += DetectorZone_OnObjectEntered;
        //detectorZone.OnObjectExited += DetectorZone_OnObjectExited;
        InputActionsProvider.OnClickStarted += InputActionsProvider_OnClickStarted;
    }

    //private void DetectorZone_OnObjectExited(GameObject obj)
    //{
    //    Grabbable grabbable = obj.GetComponent<Grabbable>();
    //    if (grabbable != null) detectedGrabbables.Remove(grabbable);
    //}
    //
    //private void DetectorZone_OnObjectEntered(GameObject obj)
    //{
    //    Grabbable grabbable = obj.GetComponent<Grabbable>();
    //    if (grabbable != null) detectedGrabbables.Add(grabbable);
    //}

    private void Update()
    {
        if (heldGrabbable == null) return;

        heldGrabbable.transform.position = holdAnchorPoint.position;
    }

    private void InputActionsProvider_OnClickStarted()
    {
        if (heldGrabbable != null)
        {
            heldGrabbable.SetGrabbed(false);
            heldGrabbable = null;
            return;
        }
        var detectedObjects = detectorZone.Objects;
        if (detectedObjects.Count == 0) return;

        var validObjects = detectedObjects.Where(obj => obj.GetComponent<Grabbable>() != null);
        if (validObjects.Count() == 0) return;

        heldGrabbable = validObjects.First().GetComponent<Grabbable>();
        heldGrabbable.SetGrabbed(true);
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //bool hit = Physics.Raycast(ray, out RaycastHit hitInfo, 10000, layerMask);
        //if (!hit) return;
        //
        //BugBait bait = hitInfo.collider.GetComponent<BugBait>();
        //if (bait != null) heldTransform = bait.transform;
    }
}
