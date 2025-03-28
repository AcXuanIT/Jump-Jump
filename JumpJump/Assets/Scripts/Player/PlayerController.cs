using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float DeadY;

    private void Update()
    {
        this.CheckDead();
    }
    public void CheckDead()
    {
        if(DeadY >= transform.position.y)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
