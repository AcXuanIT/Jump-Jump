using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingHome : MonoBehaviour
{
    [SerializeField] private GameObject menuSetting;
    [SerializeField] private Button btnBack;

    private void Start()
    {
        btnBack.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound(SoundHome.Instance.soundClickButton);
            menuSetting.SetActive(false);
        });
    }
}
