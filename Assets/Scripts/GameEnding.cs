using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1;
    public GameObject player;
    public CanvasGroup exitImageCanvasGroup;
    public CanvasGroup caughtImageCanvasGroup;
    public AudioSource exitAudio;
    public AudioSource caughtAuidon;  

    private bool isPlayerAtExit;
    private bool isPlayerCaught;
    private bool hasAudioPlayed;
    private float timer;
    private float displayDelay = 1.5f;

    public void CaughtPlayer()
    {
        isPlayerCaught = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isPlayerAtExit = true;
        }       
    }

    private void Update()
    {
        if (isPlayerAtExit)
        {
            EndLvl(exitImageCanvasGroup, false, exitAudio);
        }
        else if (isPlayerCaught)
        {
            EndLvl(caughtImageCanvasGroup, true, caughtAuidon);
        }
    }

    private void EndLvl(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        if (!hasAudioPlayed)
        {
            audioSource.Play();
            hasAudioPlayed = true;
        }

        timer += Time.deltaTime;
        imageCanvasGroup.alpha = timer / fadeDuration;

        if (timer > fadeDuration + displayDelay)
        {
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                Application.Quit();
            }  
        }
    }
}
