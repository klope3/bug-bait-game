using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BugState : State
{
    protected BugStateManager bugStateManager;

    public void Initialize(BugStateManager bugStateManager)
    {
        this.bugStateManager = bugStateManager;
    }
}
