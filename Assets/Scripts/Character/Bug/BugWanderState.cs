using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugWanderState : BugState
{
    public override void EnterState()
    {
        bugStateManager.FreeMovement.enabled = true;
        bugStateManager.ChaseMovement.enabled = false;
    }

    public override void ExitState()
    {

    }

    public override string GetDebugName()
    {
        return "wander";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(BugStateManager.STATE_FLEE, () => bugStateManager.DetectedHazard != null),
            new StateTransition(BugStateManager.STATE_CHASE, () => bugStateManager.DetectedBaitCount > 0)
        };
    }

    public override void UpdateState()
    {

    }
}
