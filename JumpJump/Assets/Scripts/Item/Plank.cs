using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    private Rigidbody2D rd;
    [SerializeField] private float desY;
    private bool isNew;
    
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
        Down();

        if(checkDestroy())
        {
            DestroyPlank();
        }
    }

    public void Down()
    {
        rd.velocity = Time.deltaTime * GameController.Instance.SpeedGame * Vector2.down;
    }
    public bool checkDestroy()
    {
        return transform.position.y <= this.desY;
    }
    public void DestroyPlank()
    {
        PoolingManager.Destroy(gameObject);
    }

}
