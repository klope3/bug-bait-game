using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugStateManager : StateManager<BugState>
{
    [field: SerializeField] public ECM2.Character Character { get; private set; }
    [SerializeField] private BugHazardSensor hazardSensor;
    [SerializeField] private BugBaitSensor baitSensor;
    [field: SerializeField] public float ChaseBaitDistance { get; private set; }
    [field: SerializeField] public float ConfusionSeconds { get; private set; }
    [field: SerializeField] public float FleeSeconds { get; private set; }
    [field: SerializeField] public BugMovementFree FreeMovement { get; private set; }
    [field: SerializeField] public BugMovementChase ChaseMovement { get; private set; }

    public static readonly string STATE_WANDER = "wander";
    public static readonly string STATE_CHASE = "chase";
    public static readonly string STATE_CONFUSED = "conf";
    public static readonly string STATE_FLEE = "flee";
    private List<BugBait> detectedBaits;
    public int DetectedBaitCount
    {
        get
        {
            if (detectedBaits == null) return 0;
            return detectedBaits.Count;
        }
    }
    public BugHazard DetectedHazard { get; set; }

    protected override void StartInitialize()
    {
        detectedBaits = new List<BugBait>();
        baitSensor.OnBaitDetected += BaitSensor_OnBaitDetected;
        baitSensor.OnBaitLost += BaitSensor_OnBaitLost;
        hazardSensor.OnHazardDetected += HazardSensor_OnHazardDetected;
    }

    private void BaitSensor_OnBaitLost(BugBait bait)
    {
        detectedBaits.Remove(bait);
    }

    private void BaitSensor_OnBaitDetected(BugBait bait)
    {
        detectedBaits.Add(bait);
    }

    private void HazardSensor_OnHazardDetected(BugHazard hazard)
    {
        DetectedHazard = hazard;
    }

    public BugBait ChooseClosestBait()
    {
        return Utils.GetExtremeFromCollection(
            detectedBaits, 
            (BugBait bait) => Vector3.Distance(bait.transform.position, Character.transform.position), 
            Utils.GetExtremeFromCollectionParam.Minimum);
    }

    protected override void EndUpdate()
    {

    }

    protected override string GetInitialStateName()
    {
        return STATE_WANDER;
    }

    protected override Dictionary<string, BugState> GetStateDictionary()
    {
        Dictionary<string, BugState> states = new Dictionary<string, BugState>();

        RegisterState(STATE_WANDER, new BugWanderState(), states);
        RegisterState(STATE_CHASE, new BugChaseState(), states);
        RegisterState(STATE_CONFUSED, new BugConfusedState(), states);
        RegisterState(STATE_FLEE, new BugFleeState(), states);

        return states;
    }
    
    private void RegisterState(string name, BugState state, Dictionary<string, BugState> dict)
    {
        dict.Add(name, state);
        state.Initialize(this);
    }
}
