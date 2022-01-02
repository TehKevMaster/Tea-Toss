using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    [SerializeField]
    float secondsLeft = 177.3f;
    [SerializeField]
    Text timeText;

    // Update is called once per frame
    void Update()
    {
        
        if (secondsLeft > 0)
        {

            secondsLeft -= Time.deltaTime;

        }
        else
        {

            secondsLeft = 0;

        }

        DisplayTime(secondsLeft);


    }

    void DisplayTime(float timeToDisplay)
    {

        if (timeToDisplay < 0)
        {

            timeToDisplay = 0;

        }

        timeText.text = string.Format("Time Remaining - {000}", timeToDisplay.ToString("0.0"));

    }

}
