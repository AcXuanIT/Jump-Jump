using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillInfo : MonoBehaviour
{
    [SerializeField] private GameObject infoSkill;
    private void OnPointerEnter()
    {
        infoSkill.SetActive(true);
    }

    private void OnPointerExit()
    {
        infoSkill.SetActive(false);
    }
}
