using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    [SerializeField] private GameObject menuRank;
    [SerializeField] private Button btnBack;
    [SerializeField] private TextMeshProUGUI textHighScore;
    private void OnEnable()
    {
        textHighScore.text = "High Score\n" + PlayerPrefs.GetInt("HighScore", 0);
    }
    private void Start()
    {
        btnBack.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            menuRank.SetActive(false);
        });
    }
}
