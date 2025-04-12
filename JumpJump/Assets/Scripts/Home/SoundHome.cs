using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHome : Singleton<SoundHome>
{
    [Header("Slider")]
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Space]
    [Header("AudioSource")]
    [SerializeField] private AudioSource soundMusic;
    [SerializeField] private AudioSource soundSFX;

    [Space]
    [Header("AudioClip")]
    [SerializeField] public AudioClip soundClickButton;
    [SerializeField] public AudioClip soundUpLevel;
    [SerializeField] public AudioClip soundBuyPlayer;

    private void Start()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("Music", 1);
        soundMusic.volume = PlayerPrefs.GetFloat("Music", 1);
        sliderSFX.value = PlayerPrefs.GetFloat("SFX", 1);
        soundSFX.volume = PlayerPrefs.GetFloat("SFX", 1);

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
        PlayerPrefs.SetFloat("SFX", volume);
        PlayerPrefs.Save();
    }
    public void PlaySound(AudioClip audio)
    {
        this.soundSFX.PlayOneShot(audio);
    }
}
