using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerCntrl : MonoBehaviour
{
    public float speed = 5;
    public float jumpForce = 7;
    [NonSerialized] public bool IsGrounded = false;
    public GameObject respawn;
    public float bonusTime;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private BonusType currentBonus = BonusType.None;
    Coroutine bonusCourotine;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(h*speed, rb.velocity.y);
        flip(h);
        RunAnim(h);
        JumpAnim();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
    }

    private void flip(float h)
    {
        if (h > 0)
        {
            spriteRenderer.flipX = true;
        } else if (h < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void RunAnim(float h)
    {
        if (h != 0 && IsGrounded)
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void JumpAnim()
    {
        if (IsGrounded)
            animator.SetBool("IsJumping", false);
        else
            animator.SetBool("IsJumping", true);

    }

    public void Death()
    {
        rb.velocity = new Vector2(0, 0);
        rb.simulated = false;
        spriteRenderer.enabled = false;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PickUpBonus(BonusType bonusType)
    {
        try { StopCoroutine(bonusCourotine); }
        catch { }
        currentBonus = bonusType;
        if(currentBonus == BonusType.Slow)
        {
            speed = 3;
        } else if (currentBonus == BonusType.SpeedUp)
        {
            speed = 6.5f;
        }
        bonusCourotine = StartCoroutine(BonusTimer());
    }

    private IEnumerator BonusTimer()
    {
        yield return new WaitForSeconds(bonusTime);
        currentBonus = BonusType.None;
        speed = 5;
    }
}
