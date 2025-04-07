using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillUI : MonoBehaviour
{
    [SerializeField] private Character Data;
    [SerializeField] private Image imageSkill;

    [SerializeField] private List<Image> listLevel;
    [SerializeField] private Sprite levelOn;
    [SerializeField] private Sprite levelOff;

    [SerializeField] private Button btnImageSkill;
    [SerializeField] private Button btnUpLevelSkill;
    private bool isSkillInfo;

    [SerializeField] private GameObject skillInfo;
    [SerializeField] private TextMeshProUGUI textInfoSkill;

    [SerializeField] private GameObject skillInfoUplevel;
    [SerializeField] private bool isUplevel;
    [SerializeField] private TextMeshProUGUI textLevelUp;
    [SerializeField] private Image imageUplevel;
    [SerializeField] private Sprite coin;
    [SerializeField] private Sprite diamond;

    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textDiamond;
    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.UpSkillUI, UpdateSkillSprite);
    }
    private void Start()
    {
        btnImageSkill.onClick.AddListener(delegate
        {
            this.skillInfo.SetActive(!isSkillInfo);
            isSkillInfo = !isSkillInfo;
        });
        btnUpLevelSkill.onClick.AddListener(delegate
        {
            if (isUplevel)
                UpLevel();

            skillInfoUplevel.SetActive(!isUplevel);
            isUplevel = !isUplevel;
        });
    }
    public void UpdateSkillSprite(object indexPlayer)
    {
        this.imageSkill.sprite = Data.characterInfos[(int)indexPlayer].skillSprite;
        textInfoSkill.text = Data.characterInfos[(int)indexPlayer].textSkillInfo;
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
        if (Data.characterInfos[(int)index].levelSkill >= 3)
        {
            this.btnUpLevelSkill.gameObject.SetActive(false);
            return;
        }

        if (Data.characterInfos[(int)index].isCoinAndDiamond)
        {
            if (Data.characterInfos[(int)index].priceUpSkill[Data.characterInfos[(int)index].levelSkill] <= PlayerPrefs.GetInt("Coins", 0))
            {
                this.btnUpLevelSkill.gameObject.SetActive(true);
            }
            else
            {
                this.btnUpLevelSkill.gameObject.SetActive(false);
            }
        }
        else
        {
            if (Data.characterInfos[(int)index].priceUpSkill[Data.characterInfos[(int)index].levelSkill] <= PlayerPrefs.GetInt("Diamonds", 0))
            {
                this.btnUpLevelSkill.gameObject.SetActive(true);
            }
            else
            {
                this.btnUpLevelSkill.gameObject.SetActive(false);
            }
        }
    }

    public void InitUplevel(int index)
    {
        if (Data.characterInfos[index].levelSkill >= 3) return;

        this.textLevelUp.text = Data.characterInfos[index].priceUpSkill[Data.characterInfos[index].levelSkill].ToString();

        if (Data.characterInfos[index].isCoinAndDiamond)
        {
            this.imageUplevel.sprite = coin;
        }
        else
        {
            this.imageUplevel.sprite = diamond;
        }
    }

    public void UpLevel()
    {
        //Debug.Log("UP UP Up level");

        int index = SkillController.Instance.IndexPlayer;

        if (Data.characterInfos[index].isCoinAndDiamond)
        {
            int coins = PlayerPrefs.GetInt("Coins", 0) - Data.characterInfos[index].priceUpSkill[Data.characterInfos[index].levelSkill];
            PlayerPrefs.SetInt("Coins", coins);
            PlayerPrefs.Save();
            textCoin.text = coins.ToString();
        }
        else
        {
            int diamond = PlayerPrefs.GetInt("Diamonds", 0) - Data.characterInfos[index].priceUpSkill[Data.characterInfos[index].levelSkill];
            PlayerPrefs.SetInt("Diamonds", diamond);
            PlayerPrefs.Save();
            textDiamond.text = diamond.ToString();
        }
        Data.characterInfos[index].levelSkill++;

        skillInfoUplevel.SetActive(false);
        isUplevel = false;

        InitUplevel(index);
        checkUpLevel(index);
        UpdateLevelSkill(index);
    }
}
