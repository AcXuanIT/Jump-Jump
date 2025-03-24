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

    private void Update()
    {
        this.UpSpeed();
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
}
