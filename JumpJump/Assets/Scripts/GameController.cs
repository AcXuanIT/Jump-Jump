using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModeGame
{
    Play,
    EndGame,
    Pause
}

public class GameController : Singleton<GameController>
{
    public ModeGame modeGame;
    [SerializeField] private float speedGame;
    private float timeer = 0f;
    private float timeDelay = 2f;

    [Space]
    [Header("Score")]
    [SerializeField] private int scoreGame;
    [SerializeField] private int coinGame;
    [SerializeField] private int diamondGame;
    private void Start()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpScore, UpdateScore);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpCoin, UpdateCoin);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpDiamond, UpdateDiamond);
    }
    private void Update()
    {
       // this.UpSpeed();
    }
    public float SpeedGame
    {
        get => this.speedGame;
        set => this.speedGame = value;
    }

    public void UpSpeed()
    {
        timeer += Time.deltaTime;
        if (timeer < timeDelay) return;
        timeer = 0;

        this.speedGame += 1f;
    }

    public void UpdateScore(object score)
    {
        this.scoreGame += (int)score;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScoreText, this.scoreGame);
    }
    public void UpdateCoin(object obj)
    {
        this.coinGame += (int)obj;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpCoinText, this.coinGame);
    }
    public void UpdateDiamond(object obj)
    {
        this.diamondGame += (int)obj;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpDiamondText, this.diamondGame);
    }
}
