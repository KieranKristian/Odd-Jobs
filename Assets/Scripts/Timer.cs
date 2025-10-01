using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI timerText;
    float remainingTime;

    public LevelManager levelManager;

    /// <summary>
    /// Counts down to 0, and if it reaches 0, calls LevelFailed on levelManager
    /// </summary>
    IEnumerator timer() {
        while (remainingTime > 0) {
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1);
            remainingTime--;
        }
        if (remainingTime <= 0) {
            levelManager.LevelFailed();
        }
    }

    /// <summary>
    /// Takes a minutes and seconds parameter and starts the timer
    /// </summary>
    public void StartTimer(int minutes, int seconds) {
        StopAllCoroutines();
        float time = (minutes * 60) + seconds; //Calculating the total time in seconds
        remainingTime = time;
        StartCoroutine(timer());
    }

    public void StopTimer() {
        StopAllCoroutines();
    }
}
