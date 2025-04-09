using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Character Data;
    [SerializeField] private Image imageSkill;

    [Space]
    [Header("Level")]
    [SerializeField] private List<Image> listLevel;
    [SerializeField] private Sprite levelOn;
    [SerializeField] private Sprite levelOff;

    [Space]
    [Header("Button")]
    [SerializeField] private Button btnImageSkill;
    [SerializeField] private Button btnUpLevelSkill;

    [Space]
    [Header("Skill")]
    [SerializeField] private GameObject skillInfo;
    [SerializeField] private TextMeshProUGUI textInfoSkill;
    [SerializeField] private GameObject skillInfoUplevel;
    [SerializeField] private bool isUplevel;
    [SerializeField] private TextMeshProUGUI textLevelUp;
    [SerializeField] private Image imageUplevel;

    [Space]
    [Header("Item")]
    [SerializeField] private Sprite coin;
    [SerializeField] private Sprite diamond;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textDiamond;

    private bool isSkillInfo;
    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpSkillUI, UpdateSkillSprite);
    }
    private void Start()
    {
        btnImageSkill.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            this.skillInfo.SetActive(!isSkillInfo);
            isSkillInfo = !isSkillInfo;
        });
        btnUpLevelSkill.onClick.AddListener(delegate
        {
            if (isUplevel)
                UpLevel();

            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            skillInfoUplevel.SetActive(!isUplevel);
            isUplevel = !isUplevel;
        });
    }
    public void UpdateSkillSprite(object indexPlayer)
    {
        var character = Data.characterInfos[(int)indexPlayer];

        this.imageSkill.sprite = character.skillSprite;
        textInfoSkill.text = character.textSkillInfo;

        UpdateLevelSkill(indexPlayer);
        checkUpLevel(indexPlayer);
        InitUplevel((int)indexPlayer);

        skillInfoUplevel.SetActive(false);
        isUplevel = false;
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

    public void checkUpLevel(object index)
    {
        var character = Data.characterInfos[(int)index];
        if (character.levelSkill >= 3)
        {
            this.btnUpLevelSkill.gameObject.SetActive(false);
            return;
        }

        if (character.isCoinAndDiamond)
        {
            if (character.priceUpSkill[character.levelSkill] <= PlayerPrefs.GetInt("Coins", 0))
                this.btnUpLevelSkill.gameObject.SetActive(true);
            else
                this.btnUpLevelSkill.gameObject.SetActive(false);
        }
        else
        {
            if (character.priceUpSkill[character.levelSkill] <= PlayerPrefs.GetInt("Diamonds", 0))
                this.btnUpLevelSkill.gameObject.SetActive(true);
            else
                this.btnUpLevelSkill.gameObject.SetActive(false);
        }
    }

    public void InitUplevel(int index)
    {
        var character = Data.characterInfos[index];
        if (character.levelSkill >= 3) return;

        this.textLevelUp.text = character.priceUpSkill[character.levelSkill].ToString();

        if (character.isCoinAndDiamond)
            this.imageUplevel.sprite = coin;
        else
            this.imageUplevel.sprite = diamond;
    }

    public void UpLevel()
    {
        int index = SkillController.Instance.IndexPlayer;
        var character = Data.characterInfos[index];

        if (character.isCoinAndDiamond)
        {
            int coins = PlayerPrefs.GetInt("Coins", 0) - character.priceUpSkill[character.levelSkill];
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
            textCoin.text = coins.ToString();
        }
        else
        {
            int diamond = PlayerPrefs.GetInt("Diamonds", 0) - character.priceUpSkill[character.levelSkill];
            PlayerPrefs.SetInt("Diamonds", diamond);
            PlayerPrefs.Save();
            textDiamond.text = diamond.ToString();
        }
        character.levelSkill++;

        skillInfoUplevel.SetActive(false);
        isUplevel = false;

        InitUplevel(index);
        checkUpLevel(index);
        UpdateLevelSkill(index);
    }
}
