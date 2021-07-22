using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerControl : MonoBehaviour
{

    // Start is called before the first frame update
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    public Animator animator;

    public int cherrynum;

    public Text textCheery;


    void Start()
    {
        
    }

    // 
    void FixedUpdate()
    {
        Movement();
    }

    void Movement() {
        
        float move = Input.GetAxis("Horizontal");
        float direction = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("running",Mathf.Abs(direction));

        if(move!=0) {
            rb.velocity = new Vector2(move*speed * Time.deltaTime,rb.velocity.y);
        }

        if(direction!=0) {
            transform.localScale = new Vector3(direction,1,1);
        }

        if(Input.GetButtonDown("Jump")) {
            rb.velocity = new Vector2(rb.velocity.x,jumpForce*Time.deltaTime);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "items")
        {
            Destroy(other.gameObject);
            cherrynum+=1;
            textCheery.text = cherrynum.ToString();
        }
    }

}
