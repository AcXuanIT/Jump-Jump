using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHome : Singleton<SoundHome>
{
    [Header("Slider")]
    [SerializeField] private Slider slider;

    [Space]
    [Header("AudioSource")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource soundSFX;

    [Space]
    [Header("AudioClip")]
    [SerializeField] private AudioClip soundClickButton;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("VolumeHome", 1f);
        audioSource.volume = savedVolume;

        slider.value = savedVolume;
        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("VolumeHome", volume);
        PlayerPrefs.Save();
    }
    public void PlaySound()
    {
        this.soundSFX.PlayOneShot(this.soundClickButton);
    }
}
