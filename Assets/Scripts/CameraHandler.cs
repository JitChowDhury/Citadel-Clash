using Unity.Cinemachine;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 30f;
    [SerializeField] private CinemachineCamera virtualCamera;


    [SerializeField] private float minOrthographicSize = 10f;
    [SerializeField] private float maxOrthographicSize = 30f;
    [SerializeField] private float zoomAmount = 2f;
    [SerializeField] private float zoomSpeed = 5f;
    private float orthographicSize;
    private float targetOrthographicSize;

    void Start()
    {
        orthographicSize = virtualCamera.Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    void Update()
    {
        HandleMovement();
        HandleZoom();

    }

    private void HandleZoom()
    {

        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;

        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        virtualCamera.Lens.OrthographicSize = orthographicSize;
        Debug.Log(orthographicSize);
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");


        Vector3 moveDir = new Vector3(x, y).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
