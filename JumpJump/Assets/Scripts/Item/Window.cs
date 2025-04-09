using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    private Rigidbody2D rd;
    [SerializeField] private float desY;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (checkDestroy())
        {
            DestroyWindow();
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
    public void DestroyWindow()
    {
        PoolingManager.Destroy(gameObject);
    }
}
