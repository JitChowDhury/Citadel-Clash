using UnityEngine;
using Unity.Cinemachine;
using Unity.VisualScripting;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }
    private CinemachineCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachineMultiChannelPerlin;
    private float timer;
    private float timerMax;
    private float startingIntensity;

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineCamera>();

        cinemachineMultiChannelPerlin = virtualCamera.GetComponentInChildren<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (timer < timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity, 0f, timer / timerMax);
            cinemachineMultiChannelPerlin.AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intensity, float timerMax)
    {
        this.timerMax = timerMax;
        timer = 0f;
        startingIntensity = intensity;
        cinemachineMultiChannelPerlin.AmplitudeGain = intensity;
    }
}
