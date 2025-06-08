using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameTimer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timerText; 
    
    private float timeRemaining = 600f;  
    private bool isCountingDown = true;
    private SceneController sc;

    void Update()
    {
        if (isCountingDown)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                isCountingDown = false;
                sc.setScene(4);
            }

            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        float minutes = Mathf.Floor(timeRemaining / 60);
        float seconds = timeRemaining % 60;
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
