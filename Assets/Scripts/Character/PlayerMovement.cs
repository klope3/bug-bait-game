using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private ECM2.Character character;
    [SerializeField] private float stopThreshold;

    private void Update()
    {
        Vector3 vecToCursor = playerInput.CursorPosition - character.transform.position;
        Vector3 moveVec = vecToCursor.magnitude > stopThreshold ? vecToCursor.normalized : Vector3.zero;
        character.SetMovementDirection(moveVec);
    }
}
