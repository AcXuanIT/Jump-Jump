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

    [Space]
    [Header("Score")]
    [SerializeField] private int scoreGame;
    [SerializeField] private int coinGame;
    [SerializeField] private int diamondGame;
    private int lastScore;

    [Space]
    [Header("Item")]
    private int planks;
    private int plankslate;
    private int coins;
    private int coinslate;
    private void Start()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpScore, UpdateScore);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpCoin, UpdateCoin);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpDiamond, UpdateDiamond);
    }
    private void Update()
    {
        this.UpSpeed();
    }
    public float SpeedGame
    {
        get => this.speedGame;
    }
    public int Planks
    {
        get => this.planks;
        set => this.planks = value;
    }
    public int PlanksLate
    {
        get => this.plankslate;
        set => this.plankslate = value;
    }
    public int Coins
    {
        get => this.coins;
        set => this.coins = value;
    }
    public int CoinsLate
    {
        get => this.coinslate;
        set => this.coinslate = value;
    }
    public void UpSpeed()
    {
        if(scoreGame >= lastScore + 50)
        {
            float x = 1f + (scoreGame)*1f / 1000;
            this.speedGame *= x;
            ObserverManager<IDGameEven>.PostEven(IDGameEven.TimeDelay, x );
            this.lastScore = scoreGame;
        }
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
