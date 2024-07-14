using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("-----AudioGameObject-----")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource backGroundMusicSource;
    [SerializeField] AudioSource SFX;

    [Header("-----AudioClipGamePlay-----")]
    public AudioClip[] MainMenuBGM;
    public AudioClip Background;
    public AudioClip LevelUp;
    public AudioClip Lose;
    public AudioClip Win;
    public AudioClip PickUpExp;
    public AudioClip PickUpHeath;
    public AudioClip PickUpCoin;
    public AudioClip TakeDmg;
    public AudioClip Warning;

    [Header("-----AudioClipWeapon-----")]
    public AudioClip Axe;
    public AudioClip Gun;
    public AudioClip Bomb;
    public AudioClip HolyBeam;
    public AudioClip Shuriken;
    public AudioClip WallSpike;
    public AudioClip ToxinZone;

    [Header("-----AudioClipWeapon-----")]
    public AudioClip BoombBat;
    public AudioClip WildBoar;

    // Start is called before the first frame update
    void Start()
    {
        backGroundMusicSource.clip = SetBGM();
        backGroundMusicSource.Play();
    }

    private void Update()
    {
        if (!backGroundMusicSource.isPlaying)
        {
            backGroundMusicSource.clip = SetBGM();
            //backGroundMusicSource.Play();
        }
    }

    public void SetActiveBackGroundMusic(bool isActive)
    {

        if (isActive)
        {
            backGroundMusicSource.Play();
        }
        else
        {
            backGroundMusicSource.Stop();
        }
    }

    private AudioClip SetBGM()
    {
        if(MainMenuBGM != null && Background == null)
        {
            backGroundMusicSource.loop = false;
            return MainMenuBGM[Random.Range(0, MainMenuBGM.Length)];
        }

        if (MainMenuBGM == null)
        {
            backGroundMusicSource.loop = true;
        }

        return Background;
    }

    public void PlaySFX(AudioClip audioClip)
    {
        SFX.PlayOneShot(audioClip);
    }

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(level)*20);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20);
    }

    public void SetSpecVolume(float level)
    {
        audioMixer.SetFloat("SpecVolume", Mathf.Log10(level) * 20);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20);
    }
}
