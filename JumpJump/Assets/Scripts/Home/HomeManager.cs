using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btnPlay;
    [SerializeField] private Button btnRankScore;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnSetting;

    [Space]
    [Header("Pannel")]
    [SerializeField] private GameObject pannelHome;
    [SerializeField] private GameObject pannelSelect;
    [SerializeField] private GameObject menuSetting;
    [SerializeField] private GameObject menuRank;

    private void Start()
    {
        btnPlay.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            this.pannelHome.SetActive(false);
            this.pannelSelect.SetActive(true);
        });
        btnRankScore.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            menuRank.SetActive(true);
        });
        btnExit.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            Application.Quit();
        });
        btnSetting.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            menuSetting.SetActive(true);
        });
    }
}
