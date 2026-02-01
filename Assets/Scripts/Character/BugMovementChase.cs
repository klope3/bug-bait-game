using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugMovementChase : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private float stopThreshold;
    [HideInInspector] public Transform targetTransform;

    private void Update()
    {
        if (targetTransform == null) return;

        Vector3 vecToTarget = targetTransform.position - character.transform.position;
        Vector3 moveVec = vecToTarget.magnitude > stopThreshold ? vecToTarget.normalized : Vector3.zero;
        character.SetMovementDirection(moveVec);
    }
}
