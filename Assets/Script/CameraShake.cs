// using UnityEngine;

// using System.Collections;

// public class CameraShake : MonoBehaviour
// {
//     public Camera freeLookCam;
//     private float shakeTimer;
//     private float shakeDuration;
//     private float shakeAmplitude;
//     private float shakeFrequency;

//     void Awake()
//     {
//         if (freeLookCam == null)
//             freeLookCam = GetComponent<CinemachineFreeLook>();
//     }

//     public void Shake(float duration, float amplitude = 2f, float frequency = 2f)
//     {
//         shakeDuration = duration;
//         shakeAmplitude = amplitude;
//         shakeFrequency = frequency;
//         shakeTimer = duration;

//         SetNoise(amplitude, frequency);
//         StopAllCoroutines();
//         StartCoroutine(ShakeCoroutine());
//     }

//     private IEnumerator ShakeCoroutine()
//     {
//         while (shakeTimer > 0)
//         {
//             shakeTimer -= Time.deltaTime;
//             yield return null;
//         }
//         SetNoise(0, 0);
//     }

//     private void SetNoise(float amplitude, float frequency)
//     {
//         for (int i = 0; i < 3; i++)
//         {
//             var rig = freeLookCam.GetRig(i);
//             var noise = rig.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//             if (noise != null)
//             {
//                 noise.m_AmplitudeGain = amplitude;
//                 noise.m_FrequencyGain = frequency;
//             }
//         }
//     }
// }