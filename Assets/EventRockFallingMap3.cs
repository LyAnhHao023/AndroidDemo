using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EventRockFallingMap3 : MonoBehaviour
{
    float timer;
    [SerializeField] float timeReFalling;
    [SerializeField] List<GameObject> fallingPrefab=new List<GameObject>();

    private CinemachineVirtualCamera camera;
    private float ShakeIntensity=2f;
    private float timeShake=0.5f;

    [SerializeField]
    AudioClip shakeSound;

    AudioManager audioManager;

    private void Start()
    {
        timer = timeReFalling;
        camera=GameObject.FindGameObjectWithTag("VirturalCamera").GetComponent<CinemachineVirtualCamera>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer<=0)
        {
            timer = timeReFalling;

            audioManager.PlaySFX(shakeSound);
            //Shake camera
            CinemachineBasicMultiChannelPerlin _cbmcp= camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _cbmcp.m_AmplitudeGain = ShakeIntensity;
            StartCoroutine(StopShake());

        }
    }

    private IEnumerator StopShake()
    {
        yield return new WaitForSeconds(timeShake);
        CinemachineBasicMultiChannelPerlin _cbmcp = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        _cbmcp.m_AmplitudeGain = 0f;

        foreach (GameObject item in fallingPrefab)
        {
            item.SetActive(true);
        }
    }
}
