using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    [SerializeField] private RuntimeAnimatorController skill02;
    [SerializeField] private RuntimeAnimatorController skill03;
    [SerializeField] private RuntimeAnimatorController skill04;
    [SerializeField] private RuntimeAnimatorController skill05;
    private Animator anim;
    private void Awake()
    {
        this.anim = effect.GetComponent<Animator>();
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.SpawnEffect, OnEffectSkill);
    }

    public void OnEffectSkill(object obj)
    {
        int index = (int)obj;

        switch(index)
        {
            case 1:
                anim.runtimeAnimatorController = skill02;
                break;
            case 2:
                anim.runtimeAnimatorController = skill03;
                break;
            case 3:
                anim.runtimeAnimatorController = skill04;
                break;
            case 4:
                anim.runtimeAnimatorController = skill05;
                break;
        }

        this.effect.SetActive(true);
        StartCoroutine(DelayEffect(1f));
    }

    IEnumerator DelayEffect(float time)
    {
        yield return new WaitForSeconds(time);
        this.effect.SetActive(false);
    }
}
