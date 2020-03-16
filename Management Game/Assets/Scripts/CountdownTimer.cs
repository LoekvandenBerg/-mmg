using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{

    public float timeLeft;
    public bool stop = true;

    private float minutes;
    private float seconds;

    [SerializeField]
    private TextMeshProUGUI timeLeftText;

    public void StartTimer(float from)
    {
        stop = false;
        timeLeft = from;
        Update();
        StartCoroutine(TimerUpdateCoroutine());
    }

    void Update()
    {
        if (stop)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        
        minutes = Mathf.Floor(timeLeft / 60);
        seconds = timeLeft % 60;
        
        if (seconds > 59) seconds = 59;
            if (minutes < 0)
            {
                stop = true;
                minutes = 0;
                seconds = 0;
            }
        
        //        fraction = (timeLeft * 100) % 100;
    }

    private IEnumerator TimerUpdateCoroutine()
    {
        while (!stop)
        {
            timeLeftText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
