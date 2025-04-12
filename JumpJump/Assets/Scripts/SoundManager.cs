using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : Singleton<SoundManager>
{
    [Header("AudioClip")]
    [SerializeField] public AudioClip audioJump; 
    [SerializeField] public AudioClip audioPickCoin;
    [SerializeField] public AudioClip audioPickDiamond;
    [SerializeField] public AudioClip audioClickButton;
    [SerializeField] public AudioClip audioPlayerFallOutMap;
    [SerializeField] public AudioClip audioPlayerDead;
    [SerializeField] public AudioClip audioBombExplosion;

    [Space]
    [Header("AudioSource")]
    [SerializeField] private AudioSource soundMusic;
    [SerializeField] private AudioSource soundSFX;
    [SerializeField] private AudioSource soundRun;

    [Space]
    [Header("Slider")]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    private void Start()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("Music", 1);
        soundMusic.volume = PlayerPrefs.GetFloat("Music", 1);
        sliderSFX.value = PlayerPrefs.GetFloat("SFX", 1);
        soundSFX.volume = PlayerPrefs.GetFloat("SFX", 1);
        soundRun.volume = PlayerPrefs.GetFloat("SFX", 1);

        sliderMusic.onValueChanged.AddListener(UpdateVolumeMusic);
        sliderSFX.onValueChanged.AddListener(UpdateVolumeSFX);
    }

    public void UpdateVolumeMusic(float volume)
    {
        sliderMusic.value = volume;
        soundMusic.volume = volume;
        PlayerPrefs.SetFloat("Music", volume);
        PlayerPrefs.Save();
    }

    public void UpdateVolumeSFX(float volume)
    {
        sliderSFX.value = volume;
        soundSFX.volume = volume;
        soundRun.volume = volume;
        PlayerPrefs.SetFloat("SFX", volume);
        PlayerPrefs.Save();
    }
    public void PlaySound(AudioClip audio)
    {
        this.soundSFX.PlayOneShot(audio);
    }

    public void PlaySoundRun()
    {
        if (soundRun.isPlaying) return;

        this.soundRun.Play();
        this.soundRun.loop = true;
    }
    public void StopSoundRun()
    {
        if (!soundRun.isPlaying) return;

        this.soundRun.Stop();
        this.soundRun.loop = false;  
    }
}
