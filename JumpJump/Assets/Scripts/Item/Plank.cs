using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plank : MonoBehaviour
{
    [SerializeField] private float desY;
    [SerializeField] private float yPostranfer;
    private Rigidbody2D rd;
    private bool isNew;
    private bool hasPushedToSkill;

    public bool IsNew
    {
        get => isNew;
        set => isNew = value;
    }
    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        isNew = true;
    }

    private void Update()
    {
        checkPosSkill();

        if(checkDestroy())
        {
            DestroyPlank();
        }
    }
    private void FixedUpdate()
    {
        Down();
    }
    public void Down()
    {
        rd.velocity = GameController.Instance.SpeedGame * Vector2.down;
    }
    public bool checkDestroy()
    {
        return transform.position.y <= this.desY;
    }
    public void DestroyPlank()
    {
        PoolingManager.Destroy(gameObject);
    }
    public void checkPosSkill()
    {
        if (!hasPushedToSkill && gameObject.transform.position.y <= yPostranfer)
        {
            this.hasPushedToSkill = true;
            this.PushPlank(this);
        }
    }
    public void PushPlank(Plank plank)
    {
        SkillController.Instance.Plank = plank;
    }
}
