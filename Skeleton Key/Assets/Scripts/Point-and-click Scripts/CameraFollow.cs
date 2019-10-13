using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTime;
    public Transform target;

    private void Update()
    {
        float posx = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime);
        //float posy = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);
        
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -24.9f, 24.9f), transform.position.y, transform.position.z);
    }
}
