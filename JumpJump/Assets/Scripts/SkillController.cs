using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillController : Singleton<SkillController>
{
    [SerializeField] private Plank newPlank;

    public Plank Plank
    {
        get => newPlank;  
        set => newPlank = value;
    }
}
