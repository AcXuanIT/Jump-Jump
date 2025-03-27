using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textDiamond;

    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpScoreText, UpTextScore);   
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpCoinText, UpTextCoin);   
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpDiamondText, UpTextDiamond);   
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
