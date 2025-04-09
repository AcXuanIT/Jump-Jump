using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Image> allHeart;

    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.Heart, UpdateHeart);
    }

    public void UpdateHeart(object obj)
    {
        MinusHeart((int)obj);
        CheckDead();
    }
    public void MinusHeart(int x)
    {
        GameController.Instance.Heart -= x;
        if (GameController.Instance.Heart < 0)
            GameController.Instance.Heart = 0;
        UpSpriteHeart();
    }

    public void UpSpriteHeart()
    {
        if(GameController.Instance.Heart == 6)
        {
            allHeart[0].sprite = sprites[0];
            allHeart[1].sprite = sprites[0];
            allHeart[2].sprite = sprites[0];
        }
        else if (GameController.Instance.Heart == 5)
        {
            allHeart[0].sprite = sprites[0];
            allHeart[1].sprite = sprites[0];
            allHeart[2].sprite = sprites[1];
        }
        else if (GameController.Instance.Heart == 4)
        {
            allHeart[0].sprite = sprites[0];
            allHeart[1].sprite = sprites[0];
            allHeart[2].sprite = sprites[2];
        }
        else if (GameController.Instance.Heart == 3)
        {
            allHeart[0].sprite = sprites[0];
            allHeart[1].sprite = sprites[1];
            allHeart[2].sprite = sprites[2];
        }
        else if (GameController.Instance.Heart == 2)
        {
            allHeart[0].sprite = sprites[0];
            allHeart[1].sprite = sprites[2];
            allHeart[2].sprite = sprites[2];
        }
        else if (GameController.Instance.Heart == 1)
        {
            allHeart[0].sprite = sprites[1];
            allHeart[1].sprite = sprites[2];
            allHeart[2].sprite = sprites[2];
        }
        else if (GameController.Instance.Heart == 0)
        {
            allHeart[0].sprite = sprites[2];
            allHeart[1].sprite = sprites[2];
            allHeart[2].sprite = sprites[2];
        }
    }
    public void CheckDead()
    {
        if (GameController.Instance.Heart == 0)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioPlayerDead);
            GameController.Instance.EndGame();
        }
    }
}
