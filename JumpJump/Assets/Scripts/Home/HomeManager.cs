using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSetting;

    [Space]
    [Header("Pannel")]
    [SerializeField] private GameObject pannelHome;
    [SerializeField] private GameObject pannelSelect;
    [SerializeField] private GameObject menuSetting;

    [Space]
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI textHighScore;

    [SerializeField] private Character character;

    private void Awake()
    {
        CheckRemoveAllInfo();
        this.textHighScore.text = "High Score:\n" + PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        btnPlay.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound(SoundHome.Instance.soundClickButton);
            this.pannelHome.SetActive(false);
            this.pannelSelect.SetActive(true);
        });
        btnExit.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound(SoundHome.Instance.soundClickButton);
            Application.Quit();
        });
        btnSetting.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound(SoundHome.Instance.soundClickButton);
            menuSetting.SetActive(true);
        });
    }
    public void CheckRemoveAllInfo()
    {
        if (!character.characterInfos[0].isOwn)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}
