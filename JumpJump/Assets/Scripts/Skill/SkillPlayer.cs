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
        //Debug.Log("1");

    }
    public void Skill02()
    {
        Debug.Log("2");
    }
    public void Skill03()
    {
        Debug.Log("3");
    }
    public void Skill04()
    {
        Debug.Log("4");
        StartCoroutine("EditSpeed", 2);

    }
    public void Skill05()
    {
        //Debug.Log("5");
        if (SkillController.Instance.Plank == null) return;
        transform.position = SkillController.Instance.Plank.transform.GetChild(0).transform.position;
    }

    IEnumerator EditSpeed(float time)
    {
        yield return new WaitForSeconds(time);

    }
}
