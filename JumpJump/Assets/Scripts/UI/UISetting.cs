using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISetting : MonoBehaviour
{
    [Space]
    [Header("Button")]
    [SerializeField] private Button btnHome;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnAgain;
    [SerializeField] private GameObject menuSetting;

    private void Start()
    {
        btnHome.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            StartCoroutine(DelayBackHome(0.05f));
        });
        btnContinue.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            menuSetting.SetActive(false);
            GameController.Instance.ContinueGame();
        });
        btnAgain.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            StartCoroutine(DelayAgainGame(0.05f));
        });
    }

    private IEnumerator DelayBackHome(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.SaveCoisAndDiamonds);
        ObserverManager<IDGameEven>.PostEven(IDGameEven.SaveScore);
        SceneManager.LoadSceneAsync("Home");
    }
    private IEnumerator DelayAgainGame(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1;
        GameController.Instance.AgainGame();
    }
}
