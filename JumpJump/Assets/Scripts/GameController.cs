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

    public float SpeedGame
    {
        get => this.speedGame;
        set => this.speedGame = value;
    }
}
