using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSpikeTrapMap2 : MonoBehaviour
{
    [SerializeField] GameObject TrapPrefab;
    [SerializeField] float timeCdTrap;
    float timer;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        timer-=Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterInfo_1 player=collision.GetComponent<CharacterInfo_1>();
        EnemyBase enemy=collision.GetComponent<EnemyBase>();
        if(timer<=0&&(player!=null|| enemy != null))
        {
            audioSource.Play();

            timer =timeCdTrap;
            TrapPrefab.SetActive(true);
            StartCoroutine(DeActiveTrap());
        }
    }

    private IEnumerator DeActiveTrap()
    {
        yield return new WaitForSeconds(10f);
        audioSource.Stop();
        TrapPrefab.SetActive(false);
    }
}
