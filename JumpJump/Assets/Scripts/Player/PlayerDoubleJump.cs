using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private Rigidbody2D rd;
    [SerializeField] private Animator animatorPlayer;
    [SerializeField] private float maxY;
 
    private bool isGround;
    private bool isDoubleJump;

    private void Awake()
    {
        this.rd = GetComponent<Rigidbody2D>();
        this.animatorPlayer = GetComponent<Animator>();
    }
    private void Update()
    {
        if (GameController.Instance.Mode != ModeGame.Play) return;

        this.CheckJump();
        this.checkPosition();
        animatorPlayer.SetFloat("yVelocity", rd.velocity.y);
    }

    public void CheckJump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isGround)
            {
                this.Jump();
                this.isGround = false;
                this.isDoubleJump = true;
                animatorPlayer.SetBool("isJumping", true);
            }
            else if(isDoubleJump)
            {
                this.Jump();
                animatorPlayer.SetBool("isJumping", false);
                this.isDoubleJump = false;
                animatorPlayer.SetBool("isJumping", true);
            }
        }
    }

    public void Jump()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioJump);
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
            this.isGround = true;
            this.isDoubleJump = false;
            animatorPlayer.SetBool("isJumping", false);
        }
    }
}
