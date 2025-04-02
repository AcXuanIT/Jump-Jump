using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundHome : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("VolumeHome", 1);
        slider.value = PlayerPrefs.GetFloat("VolumeHome", 1);

        slider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("VolumeHome",volume);
        PlayerPrefs.Save();
        audioSource.volume = volume;
        slider.value = volume;
    }
}
