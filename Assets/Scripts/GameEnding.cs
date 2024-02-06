using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1;
    public GameObject player;
    public CanvasGroup exitImageCanvasGroup;

    private bool isPlayerAtExit;
    private float timer;

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
            EndLvl();
        }
    }

    private void EndLvl()
    {
        timer += Time.deltaTime;
        exitImageCanvasGroup.alpha = timer / fadeDuration;
    }
}
