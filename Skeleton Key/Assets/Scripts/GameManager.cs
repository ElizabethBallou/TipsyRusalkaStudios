using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BookController _bookController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayBook()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Collider2D collider = Physics2D.OverlapPoint(mousePos);

        if (collider != null)
        {
            if (collider.gameObject.CompareTag("book"))
            {
                Debug.Log("you clicked a book!");
                ClickBook();
            }
        }
    }

    public void ClickBook()
    {
        _bookController.SummonBook();
    }
}
