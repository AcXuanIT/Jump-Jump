using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private bool isBoom;
    private bool isPlayerInside;
    private void Awake()
    {
        this.anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            this.isPlayerInside = true;
            if (!isBoom)
            {
                anim.SetBool("isPlayer", true);
                StartCoroutine(DelayBomb(1.15f));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.isPlayerInside = false;
        }
    }
    IEnumerator DelayBomb(float time)
    {
        yield return new WaitForSeconds(time);
        this.isBoom = true;
        SoundManager.Instance.PlaySound(SoundManager.Instance.audioBombExplosion);
        if (this.isPlayerInside && this.isBoom)
        {
            ObserverManager<IDGameEven>.PostEven(IDGameEven.Heart, 1);
            isBoom = false;
        }

        Invoke("Destroy", 1.75f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

}
