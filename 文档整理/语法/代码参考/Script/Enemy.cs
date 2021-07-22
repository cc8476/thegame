using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Animator;
    protected AudioSource DeathAudio;
    protected virtual void Start()
    {
        Animator = GetComponent<Animator>();
        DeathAudio = GetComponent<AudioSource>();
    }
    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
    }
    public void JumpOn()
    {   DeathAudio.Play();
        Animator.SetTrigger("death");
    }
}
