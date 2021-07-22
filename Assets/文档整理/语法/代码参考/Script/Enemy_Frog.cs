using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Frog : Enemy//MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    public float speed,jumpForce;
    private bool Faceleft = true;
    private float leftx, rightx;
    //private Animator Animator;
    private Collider2D Coll;
    public LayerMask Ground;


    protected override void Start()
    {
        base.Start();//
        rb = GetComponent<Rigidbody2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
        Animator = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        SwitchAnim();
    }
    void Movement()
    {
        if (Faceleft)
        {
            if (Coll.IsTouchingLayers(Ground))//青蛙跳跃
            {
                Animator.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            
            if (transform.position.x<leftx)//朝向
            {
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }
        else
        {
            if (Coll.IsTouchingLayers(Ground))//青蛙跳跃
            {
                Animator.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }

    }

    void SwitchAnim()
    {
        if (Animator.GetBool("jumping"))
        {
            if (rb.velocity.y<0.1f)
            {
                Animator.SetBool("jumping", false);
                Animator.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground)&&Animator.GetBool("falling"))
        {
            Animator.SetBool("falling", false);
        }
    }

    //public void Death()
    //{
    //    Destroy(gameObject);
    //}
    //public void JumpOn()
    //{
    //    Animator.SetTrigger("death");
    //}
}
