using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float dirX;
    private Animator anim;
    private SpriteRenderer spR;
    private BoxCollider2D collider;
    public bool isOnGround = true;
    private enum Movements { idle, runnning, jumping, falling};
    [SerializeField] private LayerMask jumpableGround;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        collider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        spR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movements state;
        dirX = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(dirX * 7.0f, playerRb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() )
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 10f);
            isOnGround = false;
        }
        if (dirX > 0f)
        {
            state = Movements.runnning;
            spR.flipX = false;

        }
        else if(dirX < 0f)
        {
            state = Movements.runnning;
            spR.flipX = true;
        }
        else
        {
            state = Movements.idle;
        }
        if (playerRb.velocity.y > 0.1f)
        {
            state = Movements.jumping;
        }
        else if (playerRb.velocity.y < -.1f)
        {
            state = Movements.falling;
        }
        anim.SetInteger("state", (int)state);
    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
