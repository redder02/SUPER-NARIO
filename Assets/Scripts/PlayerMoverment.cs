using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoverment : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private float dirX = 0f;
    private enum MovementStates { idle,running,jumping,falling};
    [SerializeField] private LayerMask jumpableground;
    [SerializeField] private float moveSpeed = 7f;  //serializefield is used to expose the variable to the unity safer than public
    
    [SerializeField] private float jumpForce = 14f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   //
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if(Input.GetButtonDown("Jump") && IsGrounded())    //GetKeyDown and GetButtonDown are used for the same purpose
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            
        }
        UpdateAnimatorState();
        
    }
    private void UpdateAnimatorState()
    {
        MovementStates state;
        if(dirX > 0f)
        {
            state = MovementStates.running; 
            sprite.flipX = false;
        }
        else if(dirX < 0f)
        {
            state = MovementStates.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementStates.idle;
            
        }
        if(rb.velocity.y > .1f)
        {
            state = MovementStates.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementStates.falling;
        }

        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f,jumpableground);
        //it makes a box slightly below the player (0.1f) and we are checking if that box is colliding with the ground or not
         
    }
}
