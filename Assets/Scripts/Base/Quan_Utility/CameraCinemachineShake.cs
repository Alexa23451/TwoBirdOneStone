//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cinemachine;

//public class CameraCinemachineShake : MonoBehaviour
//{
//    public static CameraCinemachineShake Instance;
//    private CinemachineVirtualCamera cinemachineCam;
//    private CinemachineBasicMultiChannelPerlin channelPerlin;
//    private float lastTime = 0, lastIntensity = 0;

//    private void Awake()
//    {
//        Instance = this;
//    }

//    private void Start()
//    {
//        cinemachineCam = GetComponent<CinemachineVirtualCamera>();
//        channelPerlin = cinemachineCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
//    }

//    public void SetShake(float intensity, float time)
//    {
//        channelPerlin.m_AmplitudeGain = time;
//        channelPerlin.m_FrequencyGain = intensity;
//        lastIntensity = intensity;
//        lastTime = time;
//    }

//    private void Update()
//    {
//        if (channelPerlin.m_AmplitudeGain > 0)
//        {
//            channelPerlin.m_AmplitudeGain -= Time.deltaTime;
//            channelPerlin.m_FrequencyGain = Mathf.Lerp(0, lastIntensity, channelPerlin.m_AmplitudeGain / lastTime);

//            if (channelPerlin.m_AmplitudeGain <= 0)
//            {
//                channelPerlin.m_AmplitudeGain = 0;
//            }
//        }
//    }
//}