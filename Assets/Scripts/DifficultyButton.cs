using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class DifficultyButton : MonoBehaviour
{ 
    public int difficulty;




    private Button button;
    private GameManager gameManager;
    

    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        gameManager = FindObjectOfType<GameManager>();
    }

    
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        gameManager.StartGame(difficulty);
    }



}
