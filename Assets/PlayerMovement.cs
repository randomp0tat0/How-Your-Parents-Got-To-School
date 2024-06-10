using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float Move;

    public float jump;

    public bool isjumping;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(speed *Move, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isjumping == false)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jump));
        }
        if(Input.GetKey(KeyCode.S)){
            rb.gravityScale = 1.1f;
        }
        else{
            rb.gravityScale = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isjumping = false;
        }
                if (other.gameObject.CompareTag("Fly"))
        {
            isjumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isjumping = true;
        }
       if (other.gameObject.CompareTag("Fly"))
        {
            isjumping = false;
        }
    }

}