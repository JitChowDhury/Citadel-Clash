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
}
