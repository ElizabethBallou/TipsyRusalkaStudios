using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Slot")
        {
            Debug.Log("The slot is aligned!");
            if (Input.GetButton("Submit"))
            {
                Debug.Log("You've pressed the 'X' button!");
                other.gameObject.GetComponentInParent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }
}
