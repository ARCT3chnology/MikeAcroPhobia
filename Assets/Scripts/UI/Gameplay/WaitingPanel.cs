using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingPanel : WelcomePanel
{
    private void OnEnable()
    {
        GameManager.faceOffRoundNumberIncreased = false;
        Debug.Log("Round number is: " + GameManager.getroundNumber());
        Debug.Log("Round number is: " + GameManager.faceOffRoundNumber);  
        if ((GameSettings.normalGame) && (GameManager.getroundNumber() != 5))
        {
            timer_txt.text = "";
            Invoke("StartGame",1f);
        }
        else if ((!GameSettings.normalGame) && (GameManager.faceOffRoundNumber < 3))
        {
            timer_txt.text = "";
            Invoke("StartGame", 1f);
        }
        else
        {
            if (GameManager.playerGotSameMaxVotes() && GameSettings.FaceOffGame && GameManager.faceOffRoundNumber != 3)
            {
                timer_txt.text = "";
                Invoke("StartGame", 1f);
                if (GameManager.getFaceOffRoundNumber() != 3)
                {
                }
            }
            else if (GameManager.allPlayersGotSameVote() && GameManager.faceOffRoundNumber < 3)
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
                //AudioManager.Instance.Stop("Gameplay");
                Debug.Log("Game completed");
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
                Debug.Log("Normal Game is: " + GameSettings.normalGame + "FaceOff Number" + GameManager.faceOffRoundNumber);
                if ((!GameSettings.normalGame) && GameManager.faceOffRoundNumber < 3)
                {
                    if (GameManager.faceOffRoundNumber == 0)
                        Timer("Starting FACE-OFF Round 1 in: ");
                    if (GameManager.faceOffRoundNumber == 1)
                        Timer("Starting FACE-OFF Round 2 in: ");
                    if (GameManager.faceOffRoundNumber == 2)
                        Timer("Starting FACE-OFF Round 3 in: ");
                }
                else
                {
                    Debug.Log("5 Levels are completed");
                    this.gameObject.SetActive(false);  
                }
            }
        }
    }

    public void SetText(string text)
    {
        timer_txt.text  = text;
    }
}
