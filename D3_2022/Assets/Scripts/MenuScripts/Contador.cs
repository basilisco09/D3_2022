using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Contador : MonoBehaviour
{
    EnemyLifeSystem ene;
    public GameObject textDisplay;
    public int cont = 0;

    void Start()
    {
        textDisplay.GetComponent<Text>().text = cont.ToString("00");
        
    }

    
   public bool contaMortes(int mortes)
    {
        cont+=mortes;
        textDisplay.GetComponent<Text>().text = cont.ToString("00");
        return true;
    }
}
