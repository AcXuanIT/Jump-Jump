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
            if (this.maxX <= transform.position.x)
            {
                rd.velocity = new Vector2(0, rd.velocity.y);
                return;
            }

            this.Move();
            rd.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (pressHorizontal < 0)
        {
            if (this.minX >= transform.position.x)
            {
                rd.velocity = new Vector2(0, rd.velocity.y);
                return;
            }

            this.Move();
            rd.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rd.velocity = new Vector2(0, rd.velocity.y);
        }
        this.checkSound();
    }

    public void Move()
    {
        rd.velocity = new Vector2(speed * this.pressHorizontal * Time.deltaTime, rd.velocity.y);
        this.AnimatorMovement();
    } 

    public void AnimatorMovement()
    {
        animationPlayer.SetFloat("xVelocity", Mathf.Abs(rd.velocity.x));
    }
    public void checkSound()
    {
        if (this.pressHorizontal != 0 && isGround)
            SoundManager.Instance.PlaySoundRun();
        else
            SoundManager.Instance.StopSoundRun();
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
