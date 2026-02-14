using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugConfusedState : BugState
{
    public override void EnterState()
    {
        bugStateManager.ChaseMovement.enabled = false;
        bugStateManager.FreeMovement.enabled = false;
        bugStateManager.Character.SetMovementDirection(Vector3.zero);
    }

    public override void ExitState()
    {

    }

    public override string GetDebugName()
    {
        return "confused";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(BugStateManager.STATE_CHASE, ToChaseState),
            new StateTransition(BugStateManager.STATE_WANDER, () => bugStateManager.TimeInState > bugStateManager.ConfusionSeconds)
        };
    }

    private bool ToChaseState()
    {
        float dist = bugStateManager.ChaseMovement.DistanceToTarget;
        return (dist > 0 && dist < bugStateManager.ChaseBaitDistance) || bugStateManager.DetectedBaitCount > 0;
    }

    public override void UpdateState()
    {

    }
}
