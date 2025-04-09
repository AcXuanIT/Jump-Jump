using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button btnBackHome;
    [SerializeField] private Button btnLeft;
    [SerializeField] private Button btnRight;
    [SerializeField] private Button btnSelect;
    [SerializeField] private Button btnPlay;

    [Space]
    [Header("Panel")]
    [SerializeField] private GameObject panelNotHaveCoins;
    [SerializeField] private GameObject panelSelect;
    [SerializeField] private GameObject panelHome;
    [SerializeField] private Image BackGround;

    [Space]
    [Header("Player")]
    [SerializeField] private Animator animator;
    [SerializeField] private Character characters;
    [SerializeField] private TextMeshProUGUI textInfoOwn;
    private int indexSelect; 
    private int indexPlayer; 
    [SerializeField] private Sprite coin;
    [SerializeField] private Sprite diamond;
    [SerializeField] private Image item;

    [Space]
    [Header("Item")]
    private int coins;
    private int diamonds;
    [SerializeField] private TextMeshProUGUI textCoin;
    [SerializeField] private TextMeshProUGUI textDiamond;

    private void Awake()
    {
        this.coins = PlayerPrefs.GetInt("Coins", 0);
        this.diamonds = PlayerPrefs.GetInt("Diamonds", 0);
        this.indexPlayer = PlayerPrefs.GetInt("IndexSelect", 0);
        this.indexSelect = PlayerPrefs.GetInt("IndexSelect", 0);
        SkillController.Instance.IndexPlayer = indexPlayer;

        UpText();
        UpdatePlayer();
        UpTextInfo();
        UpSkill();
    }
    private void Start()
    {
        btnBackHome.onClick.AddListener(() => this.OnClickBackHome());
        btnLeft.onClick.AddListener(() => this.ChangeCharacter(-1));
        btnRight.onClick.AddListener(() => this.ChangeCharacter(1));
        btnSelect.onClick.AddListener(() => this.OnClickSelect());
        btnPlay.onClick.AddListener(() => this.OnClickPlay());
    }
    public void OnClickBackHome()
    {
        SoundHome.Instance.PlaySound();
        this.panelHome.SetActive(true);
        this.panelSelect.SetActive(false);
    }
    public void ChangeCharacter(int x)
    {
        SoundHome.Instance.PlaySound();
        UpIndexPlayer(x);
        UpdatePlayer();
        UpTextInfo();
        UpSkill();
    }
    public void OnClickPlay()
    {
        SoundHome.Instance.PlaySound();
        PlayerPrefs.SetInt("IndexSelect", indexSelect);
        SceneManager.LoadSceneAsync("Game");
    }
    public void UpText()
    {
        this.textCoin.text = this.coins.ToString();
        this.textDiamond.text = this.diamonds.ToString();
    }
    public void UpIndexPlayer(int x)
    {
        if (indexPlayer == 0 && x == -1)
        {
            indexPlayer = characters.characterInfos.Count - 1;
        }
        else if (indexPlayer == characters.characterInfos.Count - 1 && x == 1)
        {
            indexPlayer = 0;
        }
        else
        {
            indexPlayer += x;
        }
        SkillController.Instance.IndexPlayer = indexPlayer;
    }
    public void UpdatePlayer()
    {
        animator.runtimeAnimatorController = characters.characterInfos[indexPlayer].animationHome;
        BackGround.sprite = characters.characterInfos[indexPlayer].backGround;
    }
    public void UpTextInfo()
    {
        if (characters.characterInfos[indexPlayer].isOwn)
        {
            if (indexPlayer != indexSelect)
            {
                textInfoOwn.text = "Đã sở hữu";
                item.gameObject.SetActive(false);
            }
            else
            {
                textInfoOwn.text = "Đã chọn";
                item.gameObject.SetActive(false);
            }
        }
        else
        {
            textInfoOwn.text = "  " + characters.characterInfos[indexPlayer].price.ToString();
            item.gameObject.SetActive(true);
            if (characters.characterInfos[indexPlayer].isCoinAndDiamond)
            {
                item.sprite = coin;
            }
            else
            {
                item.sprite = diamond;
            }
        }
    }
    public void OnClickSelect()
    {
        SoundHome.Instance.PlaySound();
        if (characters.characterInfos[indexPlayer].isOwn)
        {
            this.indexSelect = indexPlayer;
            textInfoOwn.text = "Đã chọn";
        }
        else
        {
            if(checkCoinsAndDiamonds())
            {
                UpText();
                characters.characterInfos[indexPlayer].isOwn = true;
                UpTextInfo();
            }
            else
            {
                panelNotHaveCoins.SetActive(true);
            }
        }
    }

    public bool checkCoinsAndDiamonds()
    {
        if (characters.characterInfos[indexPlayer].isCoinAndDiamond)
        { // coins
            if(this.coins >= this.characters.characterInfos[indexPlayer].price)
            {
                this.coins -= this.characters.characterInfos[indexPlayer].price;
                PlayerPrefs.SetInt("Coins", this.coins);
                return true;
            }
        }
        else
        { //diamonds
            if (this.diamonds >= this.characters.characterInfos[indexPlayer].price)
            {
                this.diamonds -= this.characters.characterInfos[indexPlayer].price;
                PlayerPrefs.SetInt("Diamonds", this.diamonds);
                return true;
            }
        }
        return false;
    }

    public void UpSkill()
    {
        ObserverManager<IDGameEven>.PostEven(IDGameEven.UpSkillUI, indexPlayer);
    }
}
