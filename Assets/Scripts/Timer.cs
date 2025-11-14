using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    bool timerRunning = false;
    public float currentTime;
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            currentTime += Time.deltaTime;
        }
        timerText.text = currentTime.ToString();
    }

    public void StartTimer()
    {
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
