using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    [SerializeField] private GameObject menuRank;
    [SerializeField] private Button btnBack;

    private void Start()
    {
        btnBack.onClick.AddListener(delegate
        {
            menuRank.SetActive(false);
        });
    }
}
