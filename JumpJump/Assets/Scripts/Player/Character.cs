using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Character", menuName = "Character/New Character")]
public class Character : ScriptableObject
{
    public List<CharacterInfo> characterInfos;
}

[System.Serializable]
public class CharacterInfo
{
    public RuntimeAnimatorController animationHome;
    public RuntimeAnimatorController animationGame;
    public int price;
    public Sprite backGround;
    public Sprite skillSprite;
    public List<int> priceUpSkill;
    public string textSkillInfo;
    public int levelSkill;
    public List<float> timeDelaySkill;
    [Header("True-Coin | False-Diamond")]
    public bool isCoinAndDiamond; 
    public bool isOwn;
}
