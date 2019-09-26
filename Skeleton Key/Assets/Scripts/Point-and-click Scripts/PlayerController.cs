using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameManager _gameManager;
    private Camera cam;
    private SpriteRenderer playerSprite;
    public int cursorIndex;

    public NPCController npcController;
    public Animator anim;
    public Image cursorSprite;
    public Collider2D door;
    public Collider2D bookOfBabel;
    public Collider2D bone;
    public float speed;
    
    #region RoomTransforms
    public Transform nw;
    public Transform ne;
    public Transform se;
    public Transform sw;
    #endregion
    
    private float angleLeft;
    private float angleRight;
    private Vector2 newPos;
    public Vector2 playerMinScale = new Vector3(0.16f, 0.16f);
    public Vector2 playerMaxScale = new Vector3(0.45f, 0.45f);
    public bool isMoving;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.instance;
        cam = Camera.main;
        Cursor.visible = false;
        cursorIndex = 0;
        playerSprite = GetComponent<SpriteRenderer>();
        newPos = playerSprite.transform.position;
        anim.Play("cursor_walking_anim");
        angleLeft = Mathf.Atan( (nw.position.x - sw.position.x)/(nw.position.y - sw.position.y)) * Mathf.Rad2Deg;
        angleRight = Mathf.Atan((ne.position.x - se.position.x)/(ne.position.y - se.position.y)) * Mathf.Rad2Deg;
        
        ScalePlayer();
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
            if (Inventory.instance._draggedItem.itemName == "Lock Pick")
            {
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

                if (hit.collider == door)
                {
                    SceneManager.LoadScene(1);
                }
            }

            switch (cursorIndex)
            {
                case 0:
                    SetTargetPosition();
                    break;
                case 1:
                    Debug.Log("You examine the object.");
                    _gameManager.DisplayBook();
                    cursorSprite.transform.SetAsLastSibling();
                    break;
                case 2:
                    Debug.Log("It doesn't say much.");
                    _gameManager.DisplayTextBox();
                    cursorSprite.transform.SetAsLastSibling();
                    break;
                case 3:
                    Take();
                    break;
                default:
                    break;
            }
        }
        
        //if the mouse is moving
        if (Input.GetAxis("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse X") > 0) {
            //play the cursor animation
            anim.speed = 1;
        } else {
            //pause the cursor animation
            anim.speed = 0;
        }
        
        TargetPosCheck();
        MovePlayer();
        MovingCheck();
        if (isMoving)
        {
            ScalePlayer();
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

    private bool MovingCheck()
    {
        if (new Vector2(transform.position.x, transform.position.y) != newPos) {
            isMoving = true; 
        }
        else {
            isMoving = false; 
        }
        return isMoving;
    }
    
    public void TargetPosCheck()
    {
        var targetPos = newPos;
        var targetAngleLeft = Mathf.Atan((nw.position.x - targetPos.x)/(nw.position.y - targetPos.y)) * Mathf.Rad2Deg;
        var targetAngleRight = Mathf.Atan((ne.position.x - targetPos.x)/(ne.position.y - targetPos.y)) * Mathf.Rad2Deg;
        //print("left: " + playerAngleLeft);
        //print("right: " + playerAngleRight);
        
        if (targetAngleLeft >= angleLeft || targetAngleRight <= angleRight || targetPos.y > nw.position.y)
        {
            newPos = transform.position;
            //print("You've crossed the line mister!");
        }
    }
    
    public void ScalePlayer(){
        var lerpRate = ((transform.position.y - sw.position.y) / (nw.position.y - sw.position.y));
        var playerScale = Vector2.Lerp(playerMaxScale, playerMinScale, lerpRate);
        transform.localScale = playerScale;
    }
}
