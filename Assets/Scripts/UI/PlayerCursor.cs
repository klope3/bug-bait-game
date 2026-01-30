using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private WorldPositionIndicator worldPositionIndicator;
    [SerializeField] private bool hideCursorOnAwake;

    private void Awake()
    {
        if (hideCursorOnAwake) Cursor.visible = false;
    }

    private void Update()
    {
        worldPositionIndicator.targetOverridePosition = playerInput.CursorPosition;
    }
}
