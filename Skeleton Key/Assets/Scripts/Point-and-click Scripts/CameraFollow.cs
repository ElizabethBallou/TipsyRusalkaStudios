using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    public Transform target;
    public Collider2D background;

    private Vector2 velocity;
    private Vector2 max;
    private Vector2 min;
    
    private float halfCamWidth;
    public float smoothTime;

    private void Start()
    {
        min = background.bounds.min;
        max = background.bounds.max;
        cam = GetComponent<Camera>();
        float height = cam.orthographicSize*2f;
        halfCamWidth = (cam.aspect * height)/2;
    }

    private void Update()
    {
        float posx = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime * Time.deltaTime);
        //float posy = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);
        
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x + halfCamWidth, max.x - halfCamWidth), transform.position.y, transform.position.z);
    }
}
