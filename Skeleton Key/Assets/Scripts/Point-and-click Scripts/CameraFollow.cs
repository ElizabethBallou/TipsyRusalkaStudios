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
    private float defaultCamSize = 14.7f;
    //private float startingCamSize = 27.9f;
    private Vector3 defaultCamPos = new Vector3(0.0f, -13.4f, -10.0f);
    private bool isZooming;
    private float halfCamWidth;
    public float smoothTime;

    private void Start()
    {
        min = background.bounds.min;
        max = background.bounds.max;
        cam = GetComponent<Camera>();
        isZooming = true;
    }

    private void Update()
    {
        if (isZooming)
        {
            StartCoroutine(Zoom());
        }
        
        float posx = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, smoothTime * Time.deltaTime);
        //float posy = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, smoothTime);
        
        transform.position = new Vector3(posx, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x + halfCamWidth, max.x - halfCamWidth), transform.position.y, transform.position.z);
    }

    private IEnumerator Zoom()
    {
        yield return new WaitForSeconds(2f);
        if (!Mathf.Approximately(cam.orthographicSize, defaultCamSize))
        {
            cam.transform.position = Vector3.Slerp(transform.position, defaultCamPos, 0.025f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, defaultCamSize, 0.025f);
        }
        Debug.Log("true!");
        float height = cam.orthographicSize*2f; 
        halfCamWidth = (cam.aspect * height)/2;
        isZooming = false;
    }
}
