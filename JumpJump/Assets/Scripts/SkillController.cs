using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : Singleton<SkillController>
{
    [SerializeField] private Plank newPlank;
    [SerializeField] private int indexPlayer;
    private bool isSkill01;
    [SerializeField] private List<float> ratioSkill01;
    public Plank Plank
    {
        get => newPlank;  
        set => newPlank = value;
    }
    public bool Skill01
    {
        get => isSkill01;
        set => isSkill01 = value;   
    }

    public int IndexPlayer
    {
        get => this.indexPlayer;
        set => this.indexPlayer = value;
    }
    public void SkillPlayer01(GameObject obj)
    {
        if (!isSkill01) return;

        if (GameController.Instance.Data.characterInfos[0].levelSkill == 0) return;

        float value = Random.value;

        if (value <= ratioSkill01[GameController.Instance.Data.characterInfos[0].levelSkill - 1])
        {
            if(obj.CompareTag("Coin"))
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.audioPickCoin);
                ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 10);
                ObserverManager<IDGameEven>.PostEven(IDGameEven.UpCoin, 1);
            }
            else if(obj.CompareTag("Diamond"))
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.audioPickDiamond);
                ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 100);
                ObserverManager<IDGameEven>.PostEven(IDGameEven.UpDiamond, 1);
            }
        }
    }
}
