using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private WorldPositionIndicator crosshair;
    public Vector3 CursorPosition { get; private set; }

    public void Initialize()
    {
    }

    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(InputActionsProvider.GetMousePosition());
        //check if the ray from the camera would hit an imaginary horizontal plane at y=0
        bool rayHitGroundPlane = IntersectRayWithHorizontalPlane(ray, 0, out Vector3 groundPlaneIntersection);
        if (!rayHitGroundPlane)
        {
            return;
        }

        CursorPosition = groundPlaneIntersection;
    }

    private bool IntersectRayWithHorizontalPlane(Ray ray, float planeY, out Vector3 intersection)
    {
        intersection = Vector3.zero;
        float t = (planeY - ray.origin.y) / ray.direction.y;

        // Optional: reject intersections "behind" the ray origin
        if (t < 0f)
            return false;

        intersection = ray.origin + ray.direction * t;
        return true;
    }
}
