using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rd;
    private float pressHorizontal;
    private float pressVertical;

    [SerializeField] private float speed;

    private void Awake()
    {
        this.rd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        this.Movement();
    }

    public void GetAxits()
    {
        this.pressHorizontal = Input.GetAxis("Horizontal");
    }

    public void Movement()
    {
        GetAxits();

        if (pressHorizontal > 0)
        {
            rd.velocity = this.speed * Vector2.right * Time.deltaTime;
        }
        else if (pressHorizontal < 0)
        {
            rd.velocity = this.speed * Vector2.left * Time.deltaTime;
        }
        else
            rd.velocity = Vector2.zero;
    }
}
