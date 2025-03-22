using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rd;

    private bool isJump;
    private bool isDoubleJump;

    private void Awake()
    {
        this.rd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckJump();
    }

    public void CheckJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isJump)
        {
            this.Jump();
            this.isJump = false;
            this.isDoubleJump = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isDoubleJump)
        {
            this.Jump();
            this.isDoubleJump = false;
        }
    }

    public void Jump()
    {
        this.rd.velocity = this.jumpForce * Vector2.up * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Plank"))
        {
            Debug.Log("Plank");
            this.isJump = true;
        }
    }
}
