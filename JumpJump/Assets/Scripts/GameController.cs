using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ModeGame
{
    Play,
    EndGame,
    Pause
}

public class GameController : Singleton<GameController>
{
    [SerializeField] private ModeGame modeGame;
    [SerializeField] private float speedGame;
    [SerializeField] private Character characterData;
    [SerializeField] private int heart;

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

    [Space]
    [Header("Lose")]
    [SerializeField] private UILose uiLose;
    private void Start()
    { 
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpScore, UpdateScore);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpCoin, UpdateCoin);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpDiamond, UpdateDiamond);
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.Save, SaveCoinsAndDiamonds);

        this.coinGame = PlayerPrefs.GetInt("Coins", 0);
        this.diamondGame = PlayerPrefs.GetInt("Diamonds", 0);
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpCoin, 0);
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpDiamond, 0);
    }
    private void Update()
    {
        this.UpSpeed();
    }
    private void OnDisable()
    {
        ObserverManager<IDGameEven>.RemoveAll();
    }
    //speed
    public float SpeedGame
    {
        get => this.speedGame;
    }
    //plank
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
    //coin
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
    //modeGame
    public ModeGame Mode
    {
        get => modeGame;
        set => modeGame = value;
    }
    //Item
    public int CoinGame
    {
        get => this.coinGame;
        set => this.coinGame = value;
    }
    public int DiamondGame
    {
        get => this.diamondGame;
        set => this.diamondGame = value;
    }
    public Character Data
    {
        get => this.characterData;
        set => this.characterData = value;
    }
    public int Score
    {
        get => this.scoreGame;
    }
    public int Heart
    {
        get => heart;
        set => heart = value;
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        modeGame = ModeGame.Pause;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        modeGame = ModeGame.Play;
    }


    public void EndGame()
    {
        uiLose.gameObject.SetActive(true);
        this.SaveCoinsAndDiamonds();
    }
    public void AgainGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        this.SaveCoinsAndDiamonds();
    }

    public void SaveCoinsAndDiamonds(object obj=null)
    {
        PlayerPrefs.SetInt("Coins", this.coinGame);
        PlayerPrefs.SetInt("Diamonds", this.diamondGame);
    }
}
