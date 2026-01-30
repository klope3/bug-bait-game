using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    [SerializeField] private PlayerInput tester;

    private void Awake()
    {
        InputActionsProvider.Initialize();
        tester.Initialize();
    }
}
