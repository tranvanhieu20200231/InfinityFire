using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin m_ChannelPerlin;

    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        m_ChannelPerlin = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        ResetIntensity();
    }

    public void ShakeCamera(float intensity, float shakeTime)
    {
        m_ChannelPerlin.m_AmplitudeGain = intensity;
        StartCoroutine(WaitTime(shakeTime));
    }

    IEnumerator WaitTime(float shakeTime)
    {
        yield return new WaitForSeconds(shakeTime);
        ResetIntensity();
    }

    void ResetIntensity()
    {
        m_ChannelPerlin.m_AmplitudeGain = 0;
    }
}
