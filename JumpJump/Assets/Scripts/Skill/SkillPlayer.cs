using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlayer : MonoBehaviour
{
    private void Awake()
    {
        ObserverManager<IDGameEven>.AddDesgisterEvent(IDGameEven.Skill, UseSkill);
    }

    public void UseSkill(object indexSkill)
    {

        switch((int)indexSkill)
        {
            case 0:
                this.Skill01();
                break;
            case 1:
                this.Skill02();
                break;
            case 2:
                this.Skill03();
                break;
            case 3:
                this.Skill04();
                break;
            case 4:
                this.Skill05();
                break;
        }
    }

    public void Skill01()
    {
    }
    public void Skill02()
    {
        ObserverManager<IDGameEven>.PostEven(IDGameEven.Skill2, null);
    }
    public void Skill03()
    {
        ObserverManager<IDGameEven>.PostEven(IDGameEven.Heart, -(GameController.Instance.Data.characterInfos[2].levelSkill));
    }
    public void Skill04()
    {
        StartCoroutine("EditSpeed", 4);
    }
    public void Skill05()
    {
        if (SkillController.Instance.Plank == null) return;
        transform.position = SkillController.Instance.Plank.transform.GetChild(0).transform.position;
    }

    IEnumerator EditSpeed(float time)
    {
        GameController.Instance.SpeedGame /= 2;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.TimeDelay, 0.5f);

        yield return new WaitForSeconds(time);

        GameController.Instance.SpeedGame *= 2;
        ObserverManager<IDGameEven>.PostEven(IDGameEven.TimeDelay, 2f);
    }
}
