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
        textLose.text = "Your Score : " + GameController.Instance.Score;
        btnAgain.onClick.AddListener(delegate
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        btnHome.onClick.AddListener(delegate
        {
            SceneManager.LoadSceneAsync("Home");
        });
    }
}
