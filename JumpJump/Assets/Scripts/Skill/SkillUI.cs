using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Character Data;
    [SerializeField] private Image imageSkill;

    [SerializeField] private List<Image> listLevel;
    [SerializeField] private Sprite levelOn;
    [SerializeField] private Sprite levelOff;

    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpSkillUI, UpdateSkillSprite);
    }
    public void UpdateSkillSprite(object indexPlayer)
    {
        this.imageSkill.sprite = Data.characterInfos[(int)indexPlayer].skillSprite;
        UpdateLevelSkill(indexPlayer);
    }

    public void UpdateLevelSkill(object indexPlayer)
    {
        int level = Data.characterInfos[(int)indexPlayer].levelSkill;
        for (int i = 0; i < 3;i++)
        {
            if (i < level)
                listLevel[i].sprite = levelOn;
            else
                listLevel[i].sprite = levelOff;
        }
    }
}
