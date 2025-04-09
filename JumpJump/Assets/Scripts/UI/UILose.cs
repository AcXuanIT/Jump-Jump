using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILose : MonoBehaviour
{
    [SerializeField] private Button btnAgain;
    [SerializeField] private Button btnHome;
    [SerializeField] private TextMeshProUGUI textLose;

    private void Start()
    {
        textLose.text = "Your Score  " +'\n'+ GameController.Instance.Score;
        btnAgain.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            StartCoroutine(DelayAgainGame(0.05f));
        });
        btnHome.onClick.AddListener(delegate
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.audioClickButton);
            StartCoroutine(DelayBackHome(0.05f));
        });
    }
    private IEnumerator DelayAgainGame(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator DelayBackHome(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        SceneManager.LoadSceneAsync("Home");
    }
}
