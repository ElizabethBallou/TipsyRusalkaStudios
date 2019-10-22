using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private float timeLeft;
    
    //public float imageTimer = 5.0f;
    public float timeMax;
    public TextMeshProUGUI timerUI;
    //public Image timerImage;
    public AudioSource tickingSound;
    
    public float TimeLeft
    {
        get { return timeLeft; }
        set
        {
            timeLeft = value;
            if (timeLeft <= -1)
            {
                ResetTimer();
                PickController.Instance.Reset();
            }
        }
    }

    public static TimeManager timeManager;

    private void Awake()
    {
        timeManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = timeMax + 1.0f;
        //tickingSound.Play();
        tickingSound.PlayDelayed(1.1f);
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleTimer();
    }

    void PuzzleTimer()
    {
        TimeLeft -= Time.deltaTime;
        int wholeTime = (int) timeLeft;
        timerUI.text = wholeTime.ToString();
        //Debug.Log("Timer: " + wholeTime);
    }

    /*void VisualTimer()
    {
        imageTimer -= Time.deltaTime;
        if (imageTimer <= 0)
        {
            timerImage.fillAmount -= 0.082f;
            imageTimer = 5;
        }
    }*/

    public void ResetTimer()
    {
        timeLeft = timeMax + 1.0f;
        //imageTimer = 5f;
        //timerImage.fillAmount = 1f;
    }
}
