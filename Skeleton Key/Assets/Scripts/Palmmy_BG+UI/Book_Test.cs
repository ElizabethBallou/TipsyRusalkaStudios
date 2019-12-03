using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Book_Test : MonoBehaviour
{
    private SpriteRenderer Book;
    private SpriteRenderer mySpr;
    
    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<SpriteRenderer>();
        Book = GameObject.Find("Book").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        transform.localScale *= 1.6f;
        mySpr.color = Color.red;
        mySpr.sortingLayerName = "Zoomed";
        Book.color = Color.white;
    }

    private void OnMouseExit()
    {
        transform.localScale /= 1.6f;
        mySpr.color = Color.white;
        mySpr.sortingLayerName = "Default";
        Book.color = new Color(1,1,1,0);
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("ZoomedBook");
    }
}
