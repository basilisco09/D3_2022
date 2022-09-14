using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject textDisplay;
    public int seconds = 0;
    public int minutes = 0;
    public bool running = false;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    void Awake()
    {
        StartCoroutine(TimerTake());       
    }
    
    IEnumerator TimerTake()
    {
        for (; ; )
        {
            running = true;
            textDisplay.GetComponent<Text>().text = minutes.ToString("00") + ":" + seconds.ToString("00");
            yield return new WaitForSeconds(1);
            seconds += 1;
            if (seconds % 60 == 0)
            {
                seconds = 0;
                minutes++;
            }
            running = false;
        }
    }

}
