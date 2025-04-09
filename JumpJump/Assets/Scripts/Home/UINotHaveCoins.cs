using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINotHaveCoins : MonoBehaviour
{
    [SerializeField] private Button btnBack;
    [SerializeField] private GameObject menuNotHaveCoins;

    private void Start()
    {
        btnBack.onClick.AddListener(delegate
        {
            SoundHome.Instance.PlaySound();
            menuNotHaveCoins.SetActive(false);
        });
    }

}
