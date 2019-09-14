using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private GameObject rb2d;
    private Camera cam;
    private Vector2 playerPos;
    private Vector2 currentPos;
    public int speed = 1;
    private Vector2 newPos;
    private bool isMoving;
    private SpriteRenderer playerSprite;
    
    // Start is called before the first frame update
    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        playerPos = transform.position;
        currentPos = playerPos;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SetTargetPosition();
        }
        MovePlayer();
    }

    void SetTargetPosition()
    {
        newPos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
        if(newPos.x < transform.position.x)
        {
            playerSprite.flipX = true;
        }
        if(newPos.x > transform.position.x)
        {
            playerSprite.flipX = false;
        }
    }
}
