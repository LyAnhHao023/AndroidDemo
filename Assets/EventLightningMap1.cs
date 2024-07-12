using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventLightningMap1 : MonoBehaviour
{
    [SerializeField]
    GameObject Lightning;

    [SerializeField]
    GameObject LightBeforLightning;

    [SerializeField]
    float timeLightning = 7f;

    float timer = 7;

    AudioSource audio;

    private void Start()
    {
        audio=GetComponent<AudioSource>();
    }

    void Update()
    {
        timer-=Time.deltaTime;
        if (timer <= 1)
        {
            LightBeforLightning.SetActive(true);
            StartCoroutine(LightningActive());
        }
    }

    private IEnumerator LightningActive()
    {
        yield return new WaitForSeconds(0.3f);
        audio.Play();
        Lightning.SetActive(true);
        timer = timeLightning;
    }
}
