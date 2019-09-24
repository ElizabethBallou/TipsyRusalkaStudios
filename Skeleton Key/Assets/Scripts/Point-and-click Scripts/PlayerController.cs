using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer playerSprite;
    private int cursorIndex;

    public Animator anim;
    public Image cursorSprite;
    public Vector2 newPos;
    public Collider2D bookOfBabel;
    public Collider2D bone;
    public float speed;

    public static PlayerController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cam = Camera.main;
        Cursor.visible = false;
        cursorIndex = 0;
        playerSprite = GetComponent<SpriteRenderer>();
        newPos = playerSprite.transform.position;
        anim.Play("cursor_walking_anim");
    }

    // Update is called once per frame
    void Update()
    {
        cursorSprite.transform.position = Input.mousePosition;

        if (Input.GetMouseButtonUp(1))
        {
            ChangeCursor();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            switch (cursorIndex)
            {
                case 0:
                    SetTargetPosition();
                    break;
                case 1:
                    Debug.Log("You examine the object.");
                    break;
                case 2:
                    Debug.Log("It doesn't say much.");
                    break;
                case 3:
                    Take();
                    break;
                default:
                    break;
            }
        }

        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse X") > 0) {
            anim.speed = 1;
        } else {
            anim.speed = 0;
        }

        MovePlayer();
    }

    void ChangeCursor()
    {
        if (cursorIndex < 3)
        {
            cursorIndex += 1;
        }
        else if (cursorIndex == 3)
        {
            cursorIndex = 0;
        }
        
        switch (cursorIndex)
        {
            case 0:
                anim.Play("cursor_walking_anim");
                break;
            case 1:
                anim.Play("cursor_examining_anim");
                break;
            case 2:
                anim.Play("cursor_talking_anim");
                break;
            case 3:
                anim.Play("cursor_taking_anim");
                break;
            default:
                break;
        }
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
    
    private void Take()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        if (hit.collider == bookOfBabel)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Inventory.instance.AddItem(1);
                Destroy(bookOfBabel.GetComponent<SpriteRenderer>());
            }
        }

        if (hit.collider == bone)
        {
            if(Input.GetMouseButtonUp(0))
            {
                Inventory.instance.AddItem(0);
                Destroy(bone.GetComponent<SpriteRenderer>());
            }
        }

        else if (hit.collider == null)
        {
            Debug.Log("It doesn't budge.");
        }
    }
}
