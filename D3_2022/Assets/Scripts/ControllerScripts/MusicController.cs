using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public GameObject Musica;
    public float seconds = 0;
    public bool running = false;
    public bool play = true;

    void Awake()
    {
        StartCoroutine(TimerTake());
    }

    IEnumerator TimerTake()
    {
        for (; ; )
        {
            running = true;
           
            yield return new WaitForSeconds(1);
            seconds = seconds + 1;
            if (seconds == 20)
            {
                Instantiate(Musica);
                play = false;
            }
            running = false;
        }
    }

}













