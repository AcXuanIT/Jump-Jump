using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float DeadY;
    [SerializeField] private Animator amin;
    [SerializeField] private Image imageSKill;
    [SerializeField] private GameObject skillUI;
    [SerializeField] private GameObject uiTime;
    [SerializeField] private TextMeshProUGUI textTime;
    private bool isCanUseSkill;
    private float timecurrent;
    private int indexSelectPlayer;

    private void Start()
    {
        this.indexSelectPlayer = PlayerPrefs.GetInt("IndexSelect", 0);
        this.isCanUseSkill = true;
        Init();
    }
    private void Update()
    {
        this.CheckDead();

        this.TimeSkill();

        if (Input.GetKeyDown(KeyCode.E) && this.isCanUseSkill && GameController.Instance.Data.characterInfos[indexSelectPlayer].levelSkill > 0)
        {
            ObserverManager<IDGameEven>.PostEven(IDGameEven.Skill, indexSelectPlayer);
            this.isCanUseSkill = false;
            StartCoroutine("timeDelaySkill", GameController.Instance.Data.characterInfos[indexSelectPlayer].timeDelaySkill[GameController.Instance.Data.characterInfos[indexSelectPlayer].levelSkill-1]);
        }
    }
    public void Init()
    {
        if (GameController.Instance.Data.characterInfos[indexSelectPlayer].levelSkill == 0)
        {
            skillUI.SetActive(false);
        }
        else
        {
            skillUI.SetActive(true);
            uiTime.SetActive(false);
        }
        if (indexSelectPlayer == 0)
            SkillController.Instance.Skill01 = true;
        else
            SkillController.Instance.Skill01 = false;

        amin.runtimeAnimatorController = GameController.Instance.Data.characterInfos[indexSelectPlayer].animationGame;
        imageSKill.sprite = GameController.Instance.Data.characterInfos[indexSelectPlayer].skillSprite;

    }
    public void CheckDead()
    {
        if (GameController.Instance.Mode == ModeGame.Play)
        {
            if (DeadY >= transform.position.y)
            {
                SoundManager.Instance.PlaySound(SoundManager.Instance.audioPlayerFallOutMap);
                ObserverManager<IDGameEven>.PostEven(IDGameEven.Heart, 2);
                gameObject.transform.position = SkillController.Instance.Plank.transform.GetChild(0).transform.position;
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
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioPickCoin);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 10);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpCoin, 1);
            checkSkill(PlayerPrefs.GetInt("IndexSelect", 0), collision.gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Diamond"))
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioPickDiamond);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpScore, 100);
            ObserverManager<IDGameEven>.PostEven(IDGameEven.UpDiamond, 1);
            checkSkill(PlayerPrefs.GetInt("IndexSelect", 0), collision.gameObject);
            Destroy(collision.gameObject);
        }
    }


    IEnumerator timeDelaySkill(float time)
    {
        uiTime.SetActive(true);
        this.timecurrent = time;

        yield return new WaitForSeconds(time);

        this.isCanUseSkill = true;
        uiTime.SetActive(false);
    }

    public void checkSkill(int index, GameObject obj)
    {
        if(index == 0)
        {
            SkillController.Instance.SkillPlayer01(obj);
        }
    }

    public void TimeSkill()
    {
        if (isCanUseSkill) return;

        timecurrent -= Time.deltaTime;
        textTime.text = timecurrent.ToString("F1") + "s";
    }
}
