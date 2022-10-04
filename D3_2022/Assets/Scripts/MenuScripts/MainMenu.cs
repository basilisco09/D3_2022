using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootSound;
    bool canPlay = false;

    void Update()
    {
        if(canPlay) SceneManager.LoadScene("Versao02.10");
    }

    public void PlayGame()
    {
        if(!audioSource.isPlaying) audioSource.PlayOneShot(shootSound, 1f);
        StartCoroutine(Wait(1.5f));
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        canPlay = true;
    }
}
