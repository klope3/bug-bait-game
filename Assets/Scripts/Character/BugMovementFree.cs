using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BugMovementFree : MonoBehaviour
{
    [SerializeField] private ECM2.Character character;
    [SerializeField] private BugHazardSensor sensorTrigger;
    [SerializeField] private float minTurnInterval;
    [SerializeField] private float maxTurnInterval;
    [SerializeField] private float turnRate;
    private float angle;
    private float timer;
    private float timerMax;
    private bool negative;

    private void Awake()
    {
        angle = Random.Range(0, Mathf.PI * 2);
    }

    private void OnEnable()
    {
        RandomizeMovement();
    }

    private void Update()
    {
        timer += Time.deltaTime;

            if (timer > timerMax) RandomizeMovement();

            if (!negative) angle += turnRate * Time.deltaTime;
            else angle -= turnRate * Time.deltaTime;

            Vector3 moveVec = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            character.SetMovementDirection(moveVec);
    }

    private void RandomizeMovement()
    {
        timer = 0;
        timerMax = Random.Range(minTurnInterval, maxTurnInterval);
        negative = Random.Range(0, 10) >= 5;
    }
}
