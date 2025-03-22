using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plank : MonoBehaviour
{
    private Rigidbody2D rd;
    [SerializeField] private float desY;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameController.Instance.modeGame != ModeGame.Play) return;

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
