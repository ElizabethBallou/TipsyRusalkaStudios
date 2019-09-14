using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //private GameObject rb2d;
    private Camera cam;
    private Texture2D[] cursors;
    private SpriteRenderer playerSprite;
    private Vector2 newPos;
    private int cursorIndex;
    
    public Texture2D walkCursor;
    public Texture2D examineCursor;
    public Texture2D talkCursor;
    public Texture2D takeCursor;
    
    public int speed = 1;
    
    
    // Start is called before the first frame update
    void Start()
    {
        cursors = new[] {walkCursor, examineCursor, talkCursor, takeCursor};
        
        cam = Camera.main;
        Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
        cursorIndex = 0;
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
                    Debug.Log("It doesn't budge.");
                    break;
                default:
                    break;
            }
        }
        
        MovePlayer();
    }

    void ChangeCursor()
    {
        if (cursorIndex < cursors.Length - 1)
        {
            Cursor.SetCursor(cursors[cursorIndex + 1], Vector2.zero, CursorMode.Auto);
            cursorIndex += 1;
        }
        else if (cursorIndex == cursors.Length - 1)
        {
            Cursor.SetCursor(cursors[0], Vector2.zero, CursorMode.Auto);
            cursorIndex = 0;
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
}
