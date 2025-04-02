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
    [Header("Pannel")]
    [SerializeField] private GameObject panelNotHaveCoins;
    [SerializeField] private GameObject pannelSelect;
    [SerializeField] private GameObject pannelHome;
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
        this.indexPlayer = 0;
        this.indexSelect = PlayerPrefs.GetInt("IndexPlayer", 0);
        UpTextInfo();
        this.coins = PlayerPrefs.GetInt("Coins", 0);
        this.diamonds = PlayerPrefs.GetInt("Diamonds", 0);
        UpText();
    }
    private void Start()
    {
        btnBackHome.onClick.AddListener(delegate
        {
            this.pannelHome.SetActive(true);
            this.pannelSelect.SetActive(false);
        });
        btnLeft.onClick.AddListener(delegate
        {
            UpIndexPlayer(-1);
            UpdatePlayer();
            UpTextInfo();
        });
        btnRight.onClick.AddListener(delegate
        {
            UpIndexPlayer(1);
            UpdatePlayer();
            UpTextInfo();
        });
        btnSelect.onClick.AddListener(delegate
        {
            ClickSelect();
        });
        btnPlay.onClick.AddListener(delegate
        {

            PlayerPrefs.SetInt("IndexPlayer", indexPlayer);
            SceneManager.LoadSceneAsync("Game");
        });
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
    public void ClickSelect()
    {
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
}
