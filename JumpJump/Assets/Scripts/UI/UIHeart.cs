using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private List<Image> allHeart;

    private bool isHeart01;
    private bool isHeart02;
    private bool isHeart03;

    private bool isHelfHeart01;
    private bool isHelfHeart02;
    private bool isHelfHeart03;
    private void Awake()
    {
        isHeart01 = true;
        isHeart02 = true;
        isHeart03 = true;

        isHelfHeart01 = true;
        isHelfHeart02 = true;
        isHelfHeart03 = true;

        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.Heart, UpdateHeart);
    }

    public void UpdateHeart(object obj)
    {
        if((int)obj == 1)
        {
            HalfHeart();
        }
        else if((int)obj == 2)
        {
            OneHeart();
        }
    }
    public void HalfHeart()
    {
        if(isHeart03 || isHelfHeart03)
        {
            if(isHeart03)
            {
                allHeart[2].sprite = sprites[1];
                isHeart03 = false;
            }
            else if(isHelfHeart03)
            {
                allHeart[2].sprite = sprites[2];
                isHelfHeart03 = false;
            }
        }
        else if(isHeart02 || isHelfHeart02)
        {
            if (isHeart02)
            {
                allHeart[1].sprite = sprites[1];
                isHeart02 = false;
            }
            else if (isHelfHeart02)
            {
                allHeart[1].sprite = sprites[2];
                isHelfHeart02 = false;
            }
        }
        else if(isHeart01 || isHelfHeart01)
        {
            if (isHeart01)
            {
                allHeart[0].sprite = sprites[1];
                isHeart01 = false;
            }
            else if (isHelfHeart01)
            {
                allHeart[0].sprite = sprites[2];
                isHelfHeart01 = false;
            }
        }
    }

    public void OneHeart()
    {
        if (isHeart03 || isHelfHeart03)
        {
            if(isHeart03)
            {
                allHeart[2].sprite = sprites[2];
                isHeart03 = false;
            }
            else if(isHelfHeart03)
            {
                allHeart[2].sprite = sprites[2];
                allHeart[1].sprite = sprites[1];
                isHelfHeart03 = false;
                isHeart02 = false;
            }
        }
        else if (isHeart02 || isHelfHeart02)
        {
            if (isHeart02)
            {
                allHeart[1].sprite = sprites[2];
                isHeart02 = false;
            }
            else if (isHelfHeart02)
            {
                allHeart[1].sprite = sprites[2];
                allHeart[0].sprite = sprites[1];
                isHelfHeart02 = false;
                isHeart01 = false;
            }
        }
        else if (isHeart01 || isHelfHeart01)
        {
            allHeart[0].sprite = sprites[2];
            isHeart01 = false;
            isHelfHeart01 = false;
        }
    }
}
