using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private float dirX;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        playerRb.velocity = new Vector2(dirX * 7.0f, playerRb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, 3);
        }
    }
}
