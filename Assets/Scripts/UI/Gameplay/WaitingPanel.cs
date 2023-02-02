using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPanel : WelcomePanel
{
    private void OnEnable()
    {
        if (GameManager.getroundNumber() != 5)
        {
            Invoke("StartGame",1f);
        }
        else
        {
            UIController.GameCompleted();
            Debug.Log("5 Levels are completed");
        }
    }

    private void Update()
    {
        if (GameSettings.gameStarted && GameManager.getroundNumber() < 5)
        {
            if(GameManager.getroundNumber() == 1)
                Timer("Starting Four Letter Round in: ");
            else if (GameManager.getroundNumber() == 2) 
                Timer("Starting Five Letter Round in: ");
            else if (GameManager.getroundNumber() == 3) 
                Timer("Starting Six Letter Round in: ");        
            else if (GameManager.getroundNumber() == 4) 
                Timer("Starting Seven Letter Round in: ");
        }
        else
        {
            Debug.Log("5 Levels are completed");
        }
    }
}
