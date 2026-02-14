using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugFleeState : BugState
{
    public override void EnterState()
    {
        Vector3 moveVec = Vector2.zero;
        if (bugStateManager.DetectedHazard != null)
        {
            moveVec = bugStateManager.Character.transform.position - bugStateManager.DetectedHazard.transform.position;
        }
        bugStateManager.Character.SetMovementDirection(moveVec);
        bugStateManager.DetectedHazard = null;
        bugStateManager.FreeMovement.enabled = false;
        bugStateManager.ChaseMovement.enabled = false;
    }

    public override void ExitState()
    {

    }

    public override string GetDebugName()
    {
        return "flee";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(BugStateManager.STATE_WANDER, () => bugStateManager.TimeInState > bugStateManager.FleeSeconds)
        };
    }

    public override void UpdateState()
    {

    }
}
