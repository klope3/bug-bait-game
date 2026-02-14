using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugChaseState : BugState
{
    public override void EnterState()
    {
        bugStateManager.FreeMovement.enabled = false;
        bugStateManager.ChaseMovement.enabled = true;
        BugBait closestBait = bugStateManager.ChooseClosestBait();
        if (closestBait == null) return;
        bugStateManager.ChaseMovement.targetTransform = closestBait.transform;
    }

    public override void ExitState()
    {

    }

    public override string GetDebugName()
    {
        return "chase";
    }

    public override StateTransition[] GetTransitions()
    {
        return new StateTransition[]
        {
            new StateTransition(BugStateManager.STATE_CONFUSED, () => bugStateManager.ChaseMovement.DistanceToTarget > bugStateManager.ChaseBaitDistance)
        };
    }

    public override void UpdateState()
    {

    }
}
