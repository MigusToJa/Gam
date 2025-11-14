using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerStart : MonoBehaviour
{
    public Timer timer; // Reference to the Timer script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            timer.StartTimer(); // Call the StartTimer method when the player enters the trigger
        }
    }
}
