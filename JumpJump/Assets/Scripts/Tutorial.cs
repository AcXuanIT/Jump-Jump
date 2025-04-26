using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject pannelTutorial;

    private void Start()
    {
        if(PlayerPrefs.GetInt("isTutorial",0) == 0)
        {
            GameController.Instance.PauseGame();
            this.pannelTutorial.SetActive(true);
        }
        else
        {
            this.pannelTutorial.SetActive(false);
        }
    }

    private void Update()
    {
        if(pannelTutorial.activeSelf && Input.GetKey(KeyCode.Q))
        {
            this.CloseTutorial();
        }
    }


    public void CloseTutorial()
    {
        PlayerPrefs.SetInt("isTutorial", 1);
        PlayerPrefs.Save();
        this.pannelTutorial.SetActive(false);
        GameController.Instance.ContinueGame();
    }
}
