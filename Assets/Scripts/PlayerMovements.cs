using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float dirX;
    private Animator anim;
    private SpriteRenderer spR;
    private BoxCollider2D collid;
    public bool isOnGround = true;
    
    private enum Movements { idle, runnning, jumping, falling};
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField]private PowerUp power;

    // Start is called before the first frame update
    private InputControls playeractions;
    private PlayerInput playerInput;
    [SerializeField] private MobileJoystiick joystick;
    public Vector2 MovementVector { get; internal set; }
    public event Action<Vector2> OnMovement;
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
        collid = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        spR = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        joystick.Onmove += Move;

    }
    private void Awake()
    {
        playeractions = new InputControls();
        playeractions.Player.Enable();
        playeractions.Player.Jump.performed += Jump;



    }

    // Update is called once per frame
    void Update()
    {
        Movements state;
        //dirX = Input.GetAxisRaw("Horizontal");
        Vector2 dirX = playeractions.Player.Movements.ReadValue<Vector2>();
        if (power.powerUp)
        {
            playerRb.velocity = new Vector2(dirX.x * 15.0f, playerRb.velocity.y);
            StartCoroutine(PowerUpCountDown());
        }
        else
        {
            playerRb.velocity = new Vector2(dirX.x * 7.0f, playerRb.velocity.y);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround )
        {
            jumpSound.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 8f);
            isOnGround = false;
            

        }
        if (dirX.x > 0f)
        {
            state = Movements.runnning;
            spR.flipX = false;

        }
        else if(dirX.x < 0f)
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
        else if (playerRb.velocity.y < -0.1f)
        {
            state = Movements.falling;
            
        }
        anim.SetInteger("state", (int)state);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
    }
    public void Jump(InputAction.CallbackContext context)
    {

        if (isOnGround)
        {
            jumpSound.Play();
            playerRb.velocity = new Vector2(playerRb.velocity.x, 8f);
            isOnGround = false;
        }
    }
    IEnumerator PowerUpCountDown()
    {
        yield return new WaitForSeconds(1);
        power.powerUp = false;
    }
    public void Move(Vector2 input)
    {
        MovementVector = input;
        OnMovement?.Invoke(MovementVector);
    }
}
