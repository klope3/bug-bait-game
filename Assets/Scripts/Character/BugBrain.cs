using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugBrain : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private BugMovementChase chaseMovement;
    [SerializeField] private BugMovementFree bugMovementFree;
    [SerializeField] private BugHazardSensor hazardSensor;
    [SerializeField] private BugBaitSensor baitSensor;
    [SerializeField, Tooltip("How many seconds the bug flees from a detected hazard.")] private float fleeTime;
    private Vector3 fleeVec;
    private List<BugBait> detectedBaits;

    private void Awake()
    {
        detectedBaits = new List<BugBait>();
        baitSensor.OnBaitDetected += BaitSensor_OnBaitDetected;
        baitSensor.OnBaitLost += BaitSensor_OnBaitLost;
        hazardSensor.OnHazardDetected += HazardSensor_OnHazardDetected;
    }

    private void BaitSensor_OnBaitLost(BugBait bait)
    {
        detectedBaits.Remove(bait);
        UpdateBaitBehavior();
    }

    private void BaitSensor_OnBaitDetected(BugBait bait)
    {
        detectedBaits.Add(bait);
        UpdateBaitBehavior();
    }

    private void UpdateBaitBehavior()
    {
        BugBait closestBait = ChooseClosestBait();

        if (closestBait == null)
        {
            bugMovementFree.enabled = true;
            chaseMovement.enabled = false;
            return;
        }

        chaseMovement.targetTransform = closestBait.transform;
        chaseMovement.enabled = true;
        bugMovementFree.enabled = false;
    }

    private BugBait ChooseClosestBait()
    {
        if (detectedBaits.Count == 0) return null;
        BugBait bait = detectedBaits[0];
        float smallestDist = float.MaxValue;

        foreach (BugBait b in detectedBaits)
        {
            float dist = Vector3.Distance(b.transform.position, character.transform.position);
            if (dist < smallestDist)
            {
                smallestDist = dist;
                bait = b;
            }
        }

        return bait;
    }

    private void HazardSensor_OnHazardDetected(BugHazard hazard)
    {
        fleeVec = (character.transform.position - hazard.transform.position).normalized;
        StartCoroutine(CO_Flee());
    }

    private IEnumerator CO_Flee()
    {
        chaseMovement.enabled = false;
        bugMovementFree.enabled = false;
        character.SetMovementDirection(fleeVec);

        yield return new WaitForSeconds(fleeTime);
        bugMovementFree.enabled = true;
    }
}
