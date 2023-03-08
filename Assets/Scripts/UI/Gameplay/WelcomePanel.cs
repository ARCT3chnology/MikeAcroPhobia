using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomePanel : MonoBehaviour
{
    [SerializeField] Text TimerTxt;
    [SerializeField] float CurrentTime, EndTime;
    [SerializeField] bool StartTimer;
    [SerializeField] UiController UiController;
    public Text timer_txt {get
        {
            return TimerTxt;
        }
        set { }
    }
    public UiController UIController 
    { 
        get {
            return UiController;
        }
    }
    private void OnEnable()
    {
       Invoke("StartGame",1f);
    }

    public void StartGame()
    {
        //TimerTxt.gameObject.SetActive(true);
        Debug.Log("StartGame");
        TimerTxt.text = "";
        CurrentTime = EndTime;
        StartTimer = true;
    }

    private void Update()
    {
        Timer("Starting Three Letter Round in: ");
    }

    public virtual void Timer(string text)
    {
        if (StartTimer)
        {
            if (CurrentTime >= 0)
            {
                CurrentTime -= Time.deltaTime;
                setTimerText(text);
            }
            else
            {
                onTimerComplete();
            }
        }
    }

    public virtual void onTimerComplete()
    {
        if (GameManager.getroundNumber() < 5)
        {
            switch (GameManager.getroundNumber())
            {
                case 0:
                    UiController.Start3LetterRound();
                    break;
                case 1:
                    UiController.Start4LetterRound();
                    break;
                case 2:
                    UiController.Start5LetterRound();
                    break;
                case 3:
                    UiController.Start6LetterRound();
                    break;
                case 4:
                    UiController.Start7LetterRound();
                    break;
                default:
                    break;
            }
        }
        //else if (GameManager.getroundNumber() == 5)
        //{
        //    if(GameManager.allPlayersGotSameVote())
        //    {
        //        //starting the game again as all players got the same vote.
        //        GameManager.updateRoundNumber(0);
        //        onTimerComplete();
        //    }
        //    else if (GameManager.OneplayerGotMaxVotes())
        //    {
        //        //Exiting room as one player has won the match.
        //        Debug.Log("One player got max votes");
        //    }
        //}
        else
        {
            switch (GameManager.getFaceOffRoundNumber())
            {
                case 0:
                    UiController.FaceOffRounds();
                    break;
                case 1:
                    UiController.FaceOffRounds();
                    break;
                default:
                    break;
            }
        }
        StartTimer = false;
        this.gameObject.SetActive(false);
    }

    public virtual void setTimerText(string text)
    {
        TimerTxt.text = text + Mathf.FloorToInt(CurrentTime % 60).ToString();
    }

}
