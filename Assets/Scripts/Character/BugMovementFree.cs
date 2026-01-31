using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BugMovementFree : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private BugSensorTrigger sensorTrigger;
    [SerializeField] private float minTurnInterval;
    [SerializeField] private float maxTurnInterval;
    [SerializeField] private float turnRate;
    [SerializeField, Tooltip("How many seconds the bug flees from a detected hazard.")] private float fleeTime;
    private float angle;
    private float timer;
    private float timerMax;
    private bool negative;
    [ShowInInspector, DisplayAsString] private State state;

    private enum State
    {
        Normal,
        RunFromHazard,
    }

    private void Awake()
    {
        angle = Random.Range(0, Mathf.PI * 2);
        RandomizeMovement();

        sensorTrigger.OnHazardDetected += SensorTrigger_OnHazardDetected;
    }

    private void SensorTrigger_OnHazardDetected(BugHazard hazard)
    {
        state = State.RunFromHazard;
        timer = 0;
        timerMax = fleeTime;
        character.SetMovementDirection((character.transform.position - hazard.transform.position).normalized);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (state == State.Normal)
        {
            if (timer > timerMax) RandomizeMovement();

            if (!negative) angle += turnRate * Time.deltaTime;
            else angle -= turnRate * Time.deltaTime;

            Vector3 moveVec = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            character.SetMovementDirection(moveVec);
        } else
        {
            if (timer > timerMax)
            {
                state = State.Normal;
                RandomizeMovement();
            }
        }
    }

    private void RandomizeMovement()
    {
        timer = 0;
        timerMax = Random.Range(minTurnInterval, maxTurnInterval);
        negative = Random.Range(0, 10) >= 5;
    }
}
