using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rd;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private float maxY;
 
    private bool isJump;
    private bool isDoubleJump;

    private void Awake()
    {
        this.rd = GetComponent<Rigidbody2D>();
        this.animatorPlayer = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameController.Instance.modeGame != ModeGame.Play) return;

        this.CheckJump();
        this.checkPosition();
    }

    public void CheckJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isJump)
            {
                this.Jump();
                this.isJump = false;
                this.isDoubleJump = true;
                this.animatorPlayer.SetBool("Jump", true);
            }
            else if(isDoubleJump)
            {
                this.Jump();
                this.isDoubleJump = false;
                this.animatorPlayer.SetBool("Jump", true);
            }
        }
    }

    public void Jump()
    {
        this.rd.velocity = new Vector2(rd.velocity.x, this.jumpForce);
    }

    public void checkPosition()
    {
        if (transform.position.y >= maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
            this.rd.velocity = new Vector2(rd.velocity.x, 0);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Plank"))
        {
            this.isJump = true;
            this.isDoubleJump = false;
            this.animatorPlayer.SetBool("Jump", false);
        }
    }
}
