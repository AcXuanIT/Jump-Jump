using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Space]
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textDiamond;

    [Space]
    [Header("Button")]
    [SerializeField] private Button btnSetting;
    [SerializeField] private GameObject menuSetting;

    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpScoreText, UpTextScore);   
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpCoinText, UpTextCoin);   
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpDiamondText, UpTextDiamond);   
    }

    private void Start()
    {
        btnSetting.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            GameController.Instance.PauseGame();
            menuSetting.SetActive(true);
        });
    }
    public void UpTextScore(object obj)
    {
        textScore.text = obj.ToString();
    }

    public void UpTextCoin(object obj)
    {
        if ((int)obj <= 999)
            textCoin.text = obj.ToString();
        else
            textCoin.text = "999+";
    }

    public void UpTextDiamond(object obj)
    {
        if ((int)obj <= 999)
            textDiamond.text = obj.ToString();
        else
            textDiamond.text = "999+";
    }

}
