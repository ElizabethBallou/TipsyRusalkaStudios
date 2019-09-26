using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickController : MonoBehaviour
{
    public float forceMultiplier;
    public float speed = 0.1f;
    public Collider2D keyhole;
    
    private Camera _camera;
    private Rigidbody2D _rb2d;
    public Transform startPos;
    private Quaternion _defaultRotation;
    private Quaternion _defaultCamRotation;
    
    private bool _reset;

    public static PickController Instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        _camera = Camera.main;
        _rb2d = GetComponent<Rigidbody2D>();
        _defaultRotation = transform.rotation;
        _defaultCamRotation = _camera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        var v = forceMultiplier * Input.GetAxis("Vertical");

        if (_reset)
        {
            _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, _defaultCamRotation, Time.time * speed);
        }

        if (_camera.transform.rotation == _defaultCamRotation)
        {
            _reset = false;
        }
        
        _rb2d.AddRelativeForce(new Vector2(0, v));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider == keyhole) {
            SceneManager.LoadScene(0);
        }
        else {
            Reset();
        }
    }

    public void Reset()
    {
        _reset = true;
        _camera.transform.rotation = Quaternion.RotateTowards(_camera.transform.rotation, _defaultCamRotation, Time.time * speed);
        transform.position = startPos.position;
        _rb2d.gameObject.transform.rotation = _defaultRotation;
        _rb2d.velocity = Vector3.zero;
    }
}
