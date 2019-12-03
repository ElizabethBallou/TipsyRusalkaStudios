using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shelves_Test : MonoBehaviour
{
    public SpriteRenderer BG;
    private SpriteRenderer mySpr;
    private bool isHover;

    // Start is called before the first frame update
    void Start()
    {
        mySpr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        mySpr.color = new Color(1,0.2f,0.2f,0.5f);
        isHover = true;
        //BG.color = new Color(1,1,1,0.8f);
    }

    private void OnMouseExit()
    {
        mySpr.color = new Color(1,1,1,0);
        isHover = false;
        //BG.color = Color.white;
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("ZoomedShelf");
    }
}
