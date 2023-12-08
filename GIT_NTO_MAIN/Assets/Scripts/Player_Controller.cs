using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    public bool isDo = false;
    [SerializeField] float speed = 7f;
    [SerializeField] float jumpForce = 15f;
    public bool isOneRobotDie = false;
    Rigidbody2D rb2d;
    CapsuleCollider2D body;
    BoxCollider2D legs;

    float isDoTime = 0f;
    bool isDoOff = false;

    public int countOfRobots;
    Animator animator;

    bool isAnimate = false;

    bool isDie = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        body = GetComponent<CapsuleCollider2D>();
        legs = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!isDie)
        {
            Run();
            FlipSprite();
            OffIsDo();
            CountLiveRobots();
        }
    }

    void Run()
    {
        float moveH = SimpleInput.GetAxisRaw("Horizontal");
        rb2d.velocity = new Vector2(moveH * speed, rb2d.velocity.y);

        if(!isAnimate)
        {
            if(moveH != 0f && Time.timeScale > 0f)
            {
                animator.Play("PlayerRun");
            }
            else
            {
                animator.Play("PlayerIdle");
            }
        }
    }

    public void HelpToExplosionSleepRobor(float move)
    {
        rb2d.velocity = new Vector2(move * speed, rb2d.velocity.y);
    }

    public void Jump()
    {
        bool IsGrounded = legs.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool IsKub = legs.IsTouchingLayers(LayerMask.GetMask("Kub"));
        bool IsOnEnemy = legs.IsTouchingLayers(LayerMask.GetMask("Enemy"));
        if (IsGrounded || IsOnEnemy || IsKub)
        {
            isAnimate = true;
            animator.Play("PlayerJump");
            StartCoroutine(FalsingAnimate());
            rb2d.velocity += new Vector2(0f, jumpForce);
        }
    }

    IEnumerator FalsingAnimate()
    {
        yield return new WaitForSeconds(0.5f);
        isAnimate = false;
    }

    void FlipSprite()
    {
        if (Mathf.Abs(rb2d.velocity.x) > 0.01 && Time.timeScale > 0f)
        {
            if (rb2d.velocity.x > 0f)
            {
                transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            }
            else
            {
                transform.localScale = new Vector3(-0.25f, 0.25f, 0.25f);
            }
        }
    }

    void CountLiveRobots()
    {
        countOfRobots = GameObject.FindGameObjectsWithTag("Enemy").Length + GameObject.FindGameObjectsWithTag("SleepEnemy").Length;
    }

    public void Do()
    {
        isDo = true;
    }

    void OffIsDo()
    {
        if(isDo)
        {
            if (isDoOff)
            {   
                isDoTime = 0f;
                isDoOff = false;
                isDo = false;
            }
            else
            {
                isDoTime += Time.deltaTime;
            }

            if(isDoTime >= 0.01f)
            {
                isDoOff = true;
            }
        }
    }

    public void Bam()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Die()
    {
        StartCoroutine(DieAnimate());
    }

    IEnumerator DieAnimate()
    {
        Time.timeScale = 1f;
        isDie = true;
        isAnimate = true;
        animator.Play("PlayerDie");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        StartCoroutine(ReloadLevel());
    }

    IEnumerator ReloadLevel()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
