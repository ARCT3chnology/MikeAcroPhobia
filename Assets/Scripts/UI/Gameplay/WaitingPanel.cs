using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPanel : WelcomePanel
{
    private void OnEnable()
    {
        Debug.Log("Round number is: " + GameManager.getroundNumber());  
        if ((GameSettings.normalGame) && (GameManager.getroundNumber() != 1))
        {
            timer_txt.text = "";
            Invoke("StartGame",1f);
        }
        else if ((!GameSettings.normalGame) && (GameManager.getFaceOffRoundNumber() < 3))
        {
            timer_txt.text = "";
            Invoke("StartGame", 1f);
        }
        else
        {
            if (GameManager.playerGotSameMaxVotes() && GameSettings.FaceOffGame)
            {
                if (GameManager.getFaceOffRoundNumber() != 3)
                {
                    timer_txt.text = "";
                    Invoke("StartGame", 1f);
                }
            }
            else if (GameManager.allPlayersGotSameVote())
            {
                GameManager.updateRoundNumber(0);
                UIController.restartGame();
                //Invoke("StartGame", 1f);
            }
            else if (GameManager.threePlayerGotSameVotes())
            {
                UIController.onthreePlayerGotSameVotes();
            }
            else
            {
                UIController.GameCompleted();
                Debug.Log("5 Levels are completed");
            }
        }
    }

    private void Update()
    {
        if (GameSettings.PlayerInRoom)
        {
            if (GameSettings.normalGame && GameManager.getroundNumber() < 5)
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
                Debug.Log("Normal Game is: " + GameSettings.normalGame + "FaceOff Number" + GameManager.getFaceOffRoundNumber());
                if ((!GameSettings.normalGame) && GameManager.getFaceOffRoundNumber() < 3)
                {
                    if (GameManager.getFaceOffRoundNumber() == 0)
                        Timer("Starting FACE-OFF Round 1 in: ");
                    if (GameManager.getFaceOffRoundNumber() == 1)
                        Timer("Starting FACE-OFF Round 2 in: ");
                    if (GameManager.getFaceOffRoundNumber() == 2)
                        Timer("Starting FACE-OFF Round 3 in: ");
                }
                else
                {
                    Debug.Log("5 Levels are completed");
                }
            }
        }
    }

    public void SetText(string text)
    {
        timer_txt.text  = text;
    }
}
