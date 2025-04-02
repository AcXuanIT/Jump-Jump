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
            Time.timeScale = 1;
            ObserverManager<IDGameEven>.PostEven(IDGameEven.Save);
            SceneManager.LoadSceneAsync("Home");
        });
        btnContinue.onClick.AddListener(delegate
        {
            menuSetting.SetActive(false);
            GameController.Instance.ContinueGame();
        });
        btnAgain.onClick.AddListener(delegate
        {
            Time.timeScale = 1;
            GameController.Instance.AgainGame();
        });
    }
}
