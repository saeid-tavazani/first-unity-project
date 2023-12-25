using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Added semicolon here

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Component")]
    public TextMeshProUGUI timerText;

    [Header("Timer Settings")]
    public float currentTime;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {

            currentTime = currentTime - Time.deltaTime;
            timerText.text = currentTime.ToString(); // Displaying time with two decimal places
        }
        else
        {
            timerText.text = "end game";
        }
    }
}
