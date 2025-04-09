using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rd;
    private float pressHorizontal;
    private float pressVertical;
    private bool isGround;

    [SerializeField] private float speed;
    [SerializeField] private Animator animationPlayer;

    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    private void Awake()
    {
        this.rd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (GameController.Instance.Mode != ModeGame.Play) return;

        this.GetAxits();
        this.Movement();
    }

    public void GetAxits()
    {
        this.pressHorizontal = Input.GetAxis("Horizontal");
    }

    public void Movement()
    {
        if (pressHorizontal != 0)
        {
            if ((pressHorizontal > 0 && transform.position.x >= maxX) ||
                (pressHorizontal < 0 && transform.position.x <= minX))
            {
                rd.velocity = new Vector2(0, rd.velocity.y);
                return;
            }
            Move();
            FlipSprite();
        }
        else
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        CheckSound();
    }

    public void Move()
    {
        rd.velocity = new Vector2(speed * this.pressHorizontal, rd.velocity.y);
        this.AnimatorMovement();
    } 

    public void AnimatorMovement()
    {
        animationPlayer.SetFloat("xVelocity", Mathf.Abs(rd.velocity.x));
    }
    public void CheckSound()
    {
        if (this.pressHorizontal != 0 && isGround)
            SoundManager.Instance.PlaySoundRun();
        else
            SoundManager.Instance.StopSoundRun();
    }
    public void FlipSprite()
    {
        Vector3 scale = transform.localScale;
        if (pressHorizontal > 0 && scale.x < 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (pressHorizontal < 0 && scale.x > 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Plank"))
        {
            this.isGround = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Plank"))
        {
            this.isGround = false ;
        }
    }
}
