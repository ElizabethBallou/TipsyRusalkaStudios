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

    public GameObject winUI;
    public GameObject nextUI;
    public GameObject levelHolder;

    private Camera _camera;
    private Rigidbody2D _rb2d;
    public Transform startPos;
    private Quaternion _defaultRotation;
    private Quaternion _defaultCamRotation;
    private int levelIndex;
    private List<GameObject> levels = new List<GameObject>();
    
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

        for (int i = 0; i < levelHolder.transform.childCount; i++)
        {
            levels.Add(levelHolder.transform.GetChild(i).gameObject);
        }
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

        if (Input.GetKey(KeyCode.R))
        {
            PlayAgain();
        }
        
        _rb2d.AddRelativeForce(new Vector2(0, v));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider == keyhole) {
            Win();
        }
        else {
            Reset();
            TimeManager.timeManager.ResetTimer();
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

    public void Win()
    {
        if (levelIndex < 2)
        {
            nextUI.SetActive(true);
            TimeManager.timeManager.tickingSound.Stop();
        }
        else
        {
            winUI.SetActive(true);
            TimeManager.timeManager.tickingSound.Stop();
        }
    }

    public void NextLevel()
    {
        levels[levelIndex].SetActive(false);
        levelIndex++;
        levels[levelIndex].SetActive(true);
        nextUI.SetActive(false);
        Reset();
        TimeManager.timeManager.ResetTimer();
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }
}
