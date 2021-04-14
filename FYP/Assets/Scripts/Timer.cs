using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 45;
    float seconds;
    public bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    private void Start()
    {
        timerText.text = timeRemaining.ToString();
        timerIsRunning = true;
    }
    void Update()
    {
        CountDown();
        UpdateText();
    }

    private void CountDown()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    private void UpdateText()
    {
        seconds = Mathf.FloorToInt(timeRemaining % 60);

        timerText.text = seconds.ToString() + "s";
    }

    public float GetTime()
    {
        return timeRemaining%60;
    }
}
