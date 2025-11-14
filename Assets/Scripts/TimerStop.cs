using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStop : MonoBehaviour
{
    public Timer timer; // Reference to the Timer script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer.StopTimer(); // Call the StopTimer method when the player enters the trigger
        }
    }
}
