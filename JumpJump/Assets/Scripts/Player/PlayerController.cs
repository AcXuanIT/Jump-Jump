using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float DeadY;
    [SerializeField] private Animator amin;
    private void Start()
    {
        Init();
    }
    private void Update()
    {
        this.CheckDead();
    }
    public void Init()
    {
        amin.runtimeAnimatorController = GameController.Instance.Data.characterInfos[PlayerPrefs.GetInt("IndexPlayer", 0)].animationGame;
    }
    public void CheckDead()
    {
        if (GameController.Instance.Mode == ModeGame.Play)
        {
            if (DeadY >= transform.position.y)
            {
                ObserverManager<IDGameEven>.PostEven(IDGameEven.Heart, 2);
                gameObject.transform.position = Vector3.zero;
            }
        }
        else if(GameController.Instance.Mode == ModeGame.EndGame)
        {
            transform.position = Vector2.zero;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameController.Instance.Mode != ModeGame.Play) return;

        if (collision.CompareTag("Plank"))
        {
            if (collision.TryGetComponent(out Plank plank))
            {
                if (plank.IsNew)
                {
                    plank.IsNew = false;
                    ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 1);
                }
            }
        }
        else if (collision.CompareTag("Coin"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioCoins);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 10);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpCoin, 1);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Diamond"))
        {
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 100);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpDiamond, 1);
            Destroy(collision.gameObject);
        }
    }
}
