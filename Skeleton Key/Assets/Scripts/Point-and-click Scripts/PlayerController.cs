using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Camera cam;
    private SpriteRenderer playerSprite;

    public NPCController npcController;
    
    public Animator anim;
    public Image cursorSprite;
    public int cursorIndex;
    public float speed;
    //public Collider2D door;
    //public Collider2D bookOfBabel;
    //public Collider2D bone;

    #region PlayerScaling
    public Transform northWall;
    public Transform southWall;
    public Vector2 playerMinScale = new Vector3(0.16f, 0.16f);
    public Vector2 playerMaxScale = new Vector3(0.45f, 0.45f);
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false;
        cursorIndex = 0;
        playerSprite = GetComponent<SpriteRenderer>();
        CalculateSpace.instance.newPos = playerSprite.transform.position;
        anim.Play("cursor_walking_anim");
        
        ScalePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateCursor();
        
        if (Input.GetMouseButtonUp(1))
        {
            ChangeCursor();
        }
        
        if (Input.GetMouseButtonUp(0))
        { 
            LeftClick();
        }

        if (CalculateSpace.instance.TargetIsFloor())
        {
            MovePlayer();
        }

        if (IsMoving())
        {
            ScalePlayer();
        }
        
    }

    void LeftClick()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        //if (Inventory.instance._draggedItem.itemName == "Lock Pick")
        //{
            

            /*if (hit.collider == door)
            {
                SceneManager.LoadScene(1);
            }*/
        //}

        switch (cursorIndex)
        {
            case 0:
                CalculateSpace.instance.SetTargetPosition();
                break;
            case 1:
                UIManager.instance.ExamineItem();
                Debug.Log("You examine the object.");
                cursorSprite.transform.SetAsLastSibling();
                break;
            case 2:
                Debug.Log("It doesn't say much.");
                UIManager.instance.OpenNPCDialogue();
                cursorSprite.transform.SetAsLastSibling();
                break;
            case 3:
                UIManager.instance.SummonBook();
                cursorSprite.transform.SetAsLastSibling();
                Take();
                break;
            default:
                break;
        }
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

    void AnimateCursor()
    {
        cursorSprite.transform.position = Input.mousePosition;
        
        //if the mouse is moving
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse X") > 0) {
            //play the cursor animation
            anim.speed = 1;
        } else {
            //pause the cursor animation
            anim.speed = 0;
        }
    }
    
    void MovePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, CalculateSpace.instance.newPos, Time.deltaTime * speed);
        if(CalculateSpace.instance.newPos.x < transform.position.x)
        {
            playerSprite.flipX = false;
        }
        if(CalculateSpace.instance.newPos.x > transform.position.x)
        {
            playerSprite.flipX = true;
        }
    }
    
    private bool IsMoving()
    {
        return (new Vector2(transform.position.x, transform.position.y) != CalculateSpace.instance.newPos);
    }

    private void ScalePlayer(){
        var lerpRate = ((transform.position.y - southWall.position.y) / (northWall.position.y - southWall.position.y));
        var playerScale = Vector2.Lerp(playerMaxScale, playerMinScale, lerpRate);
        transform.localScale = playerScale;
    }
    
    private void Take()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        
        /*if (hit.collider == bookOfBabel)
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
        }*/

        if (hit.collider == null)
        {
            Debug.Log("It doesn't budge.");
        }
    }
}
