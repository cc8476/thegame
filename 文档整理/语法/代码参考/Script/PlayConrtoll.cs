using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
//ctrl+R手动编译脚本
public class PlayConrtoll : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private Animator animator;
    public float speed = 10;
    public float jumpforce;
    public LayerMask Ground;
    public Collider2D coll;
    float horizontalmove;
    float facedirection;
    public int Score;
    public Text Cherrynumber;
    public bool Ishurt;//false
    public AudioSource jumpaudio, hurtaudio,charryaudio;
    public Collider2D Discoll;
    public Transform CellingCheck,GroundCheck;
    private bool isGround;
    private int extraJump;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        //if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
        //    jumpaudio.Play();
        //    animator.SetBool("jumping", true);
        //}
        //Jump();
        Crouch();
        Jump();
        
    }

    private void FixedUpdate()
    {
        //跳跃
        //if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        //{
        //    rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
        //    jumpaudio.Play();
        //    animator.SetBool("jumping", true);
        //}
        if (!Ishurt)
        {
            Movement();
        }
        
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, Ground);
        
    }

    void Movement()
    {

        horizontalmove = Input.GetAxisRaw("Horizontal");
        facedirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
        animator.SetFloat("running", Mathf.Abs(facedirection));
        if (facedirection !=0)
        {
            transform.localScale = new Vector3(facedirection, 1, 1);
        }
        //Crouch();
    }

    //动画切换
    void SwitchAnim()
    {
        animator.SetBool("idle", false);
        if (rb.velocity.y<0.1f&&!coll.IsTouchingLayers(Ground))
        {
            animator.SetBool("falling", true);
        }
        if (animator.GetBool("jumping"))
        {   animator.SetBool("idle", false);
            if (rb.velocity.y < 0)
            {
                animator.SetBool("jumping", false);
                animator.SetBool("falling", true);
            }
        }
        //受伤
        else if (Ishurt)
        {
           
            animator.SetBool("hurt", true);
            if (Mathf.Abs(rb.velocity.x) < 0.5f)
            {
                
                Ishurt = false;
                animator.SetBool("hurt", false);
                animator.SetBool("idle", true);
                
                Ishurt = false;
            }
        }
        else if (coll.IsTouchingLayers(Ground))
        {
            animator.SetBool("falling", false);
            animator.SetBool("idle", true);
        }

    }
    //碰撞触发器
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集
        if (collision.tag == "Collection")
        {
            
            //Destroy(collision.gameObject);
            //Score++;
            collision.GetComponent<Animator>().Play("isGot");
            //Cherrynumber.text=Score.ToString();
            //charryaudio.Play();
            Debug.Log(Score.ToString());
        }
        if (collision.tag=="DeathLine")
        {
            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 1f);
        }

    }
    //消灭怪物
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Enemies")
        {
            //Enemy_Frog frog = collision.gameObject.GetComponent<Enemy_Frog>();
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (animator.GetBool("falling"))
            {
                //frog.JumpOn();
                enemy.JumpOn();
                //rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                rb.velocity = Vector2.up * jumpforce;//new Vertor2 (0,1);
                animator.SetBool("jumping", true);
            }//hurt
            else if(transform.position.x<collision.transform.position.x)
            {
                
                rb.velocity = new Vector2(-2, rb.velocity.y);
                Ishurt = true;
                hurtaudio.Play();//受伤音效
            }
            else if (transform.position.x > collision.transform.position.x)
            {
                
                rb.velocity = new Vector2(2, rb.velocity.y);
                Ishurt = true;
                hurtaudio.Play();//受伤音效
            }

        }
    }
  
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position,0.2f,Ground))
        {
            if (Input.GetButton("Crouch"))
            {
                animator.SetBool("crouching", true);
                Discoll.enabled = false;
            }
            else 
            {
                animator.SetBool("crouching", false);
                Discoll.enabled = true;
            }
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
    /*void Jump()
    {
        //跳跃
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(Ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpaudio.Play();
            animator.SetBool("jumping", true);
        }
    }*/
    public void ScoreCount()
    {
        Score++;
        Cherrynumber.text = Score.ToString();
        //charryaudio.Play();
    }
    public void PlayAudio()
    {
        charryaudio.Play();
    }
    
    void Jump()
    {
        if (isGround)
        {
            extraJump = 0;//二段跳-1

        }
        if (Input.GetButtonDown("Jump") && extraJump>0)
        {
            rb.velocity = Vector2.up * jumpforce;//new Vertor2 (0,1);
            extraJump--;
            animator.SetBool("jumping", true);
            jumpaudio.Play();
        }
        if (Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            rb.velocity = Vector2.up * jumpforce;//new Vertor2 (0,1);
            animator.SetBool("jumping", true);
            jumpaudio.Play();
        }
    }
}
