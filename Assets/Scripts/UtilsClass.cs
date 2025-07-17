using System;
using Unity.Mathematics;
using UnityEngine;

public static class UtilsClass
{
    private static Camera mainCamera;
    public static Vector3 GetMousePosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);//transform to worldpoint insted of screenPos
        mousePosition.z = 0f;
        return mousePosition;
    }

    public static Vector3 GetRandomDir()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degrees = radians * Mathf.Rad2Deg;
        return degrees;
    }
}
