using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] InputField normalGameInputField;
    [SerializeField] InputField faceOffGameInputField;
    [SerializeField] UiController uiController;
    [SerializeField] Timer _gamePlayTimer;
    [SerializeField] int _noOfanswerSubmitted;

    public int noOfAnswersSubmitted
    {
        get
        {
            return _noOfanswerSubmitted;
        }
        set
        {
            _noOfanswerSubmitted = value;
        }
    }
    public Timer gamePlayTimer 
    {
        get
        {
            return _gamePlayTimer;
        }
        set { 
            _gamePlayTimer = value;}
    }
    
    public void setAnswer()
    {
        if (GameSettings.normalGame)
        {
            stats = new ExitGames.Client.Photon.Hashtable();
            stats[GameSettings.PlAYER_ANSWER] = normalGameInputField.text.ToString();
            stats[GameSettings.ANSWER_SUBMITTED] = false;
            //Debug.Log("Text is: " + normalGameInputField.text);
            PhotonNetwork.SetPlayerCustomProperties(stats);
        }
        else
        {
            stats = new ExitGames.Client.Photon.Hashtable();
            stats[GameSettings.PlAYER_ANSWER] = faceOffGameInputField.text.ToString();
            PhotonNetwork.SetPlayerCustomProperties(stats);
        }
        //Debug.Log("setting answer");
        //Debug.Log(PhotonNetwork.PlayerList[0].NickName + "setting answer");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps[GameSettings.ANSWER_SUBMITTED]!=null)
        {
            bool state = (bool)changedProps[GameSettings.ANSWER_SUBMITTED];
            //Debug.Log("Answer Submitted is: " + state);
            if (state)
            {
                if (targetPlayer.IsLocal)
                {
                    uiController.updateAnswerOnPlayer(true);
                    uiController.turnOffTextPanel(false);
                }
                else if ((bool)targetPlayer.CustomProperties[GameSettings.ANSWER_SUBMITTED] == true)
                {
                    uiController.updateAnswerOnPlayer(true);
                }
            }
        }
    }

    public static int getroundNumber()
    {
        return (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ROUND_NUMBER];
    }

    public static int getFaceOffRoundNumber()
    {
        return (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.FACEOFF_ROUND_NUMBER];
    }

    public static void updateRoundNumber()
    {
        int roundNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ROUND_NUMBER];
        roundNumber++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.ROUND_NUMBER, roundNumber } });
    }
    public static void updateRoundNumber(int number)
    {
        Debug.Log("Round Number set to: " + number);
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.ROUND_NUMBER, number } });
    }



    public static void updateFaceOffRoundNumber()
    {
        int roundNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.FACEOFF_ROUND_NUMBER];
        roundNumber++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.FACEOFF_ROUND_NUMBER, roundNumber } });
    }
    public static void updateAnswersSubmittedNumber()
    {
        int answersSubmitted = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.NO_OF_ANSWERS_SUBMITTED];
        answersSubmitted++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.NO_OF_ANSWERS_SUBMITTED, answersSubmitted } });
    }
    public static void updateAnswersSubmittedNumber(int number)
    {
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.NO_OF_ANSWERS_SUBMITTED, number } });
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if(propertiesThatChanged[GameSettings.NO_OF_ANSWERS_SUBMITTED] != null)
        {
            //Debug.Log("No of answers: " + (int)propertiesThatChanged[GameSettings.NO_OF_ANSWERS_SUBMITTED]);
            if((int)propertiesThatChanged[GameSettings.NO_OF_ANSWERS_SUBMITTED] == 4)
                uiController.votingPanel.voteTimer.StartTimer();
        }
            
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
    }

    public void OnAnswerTimeComplete()
    {
        if (GameSettings.normalGame)
        {
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                uiController.RPC_UpdateAnswerOnplayer(PhotonNetwork.PlayerList[i],false);
            }
            uiController.turnOffTextPanel(true);
            updateAnswersSubmittedNumber(4);
        }
        else
        {
            uiController.turnOffTextPanelFaceOff();
        }
    }

    /// <summary>
    /// this fucntion is called on submit button in threeletter round panel.
    /// </summary>
    public void onClick_SubmitButton()
    {
        uiController.votingPanel.submitPressed = true;
        Debug.Log("Instantiating from submit");

        if (GameSettings.normalGame)
        {
            stats = new ExitGames.Client.Photon.Hashtable();
            stats[GameSettings.ANSWER_SUBMITTED] = true;
            PhotonNetwork.SetPlayerCustomProperties(stats);
            updateAnswersSubmittedNumber();

        }
        else
        {
            uiController.turnOffTextPanelFaceOff();
        }
    }


    public void OnVotingTimeComplete_FaceOff()
    {
        uiController.faceOffMenu.DisableVotingOption();
        //uiController.turnOffTextPanelFaceOff();

    }

    public static bool allPlayersGotSameVote()
    {
        bool state;
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        state = allVotes.ToList().Distinct().Count() == 1 ? true : false;
        return state;
    }

    public static bool OneplayerGotMaxVotes() 
    {
        bool state;
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        state = maxCount == 1? true: false;
        return state;
    }

    public static bool playerGotSameMaxVotes()
    {
        bool state;
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        //Debug.Log("Players in lobby: " + allVotes.Length);
        //Debug.Log("Players array count is: " + GameSettings.PlayerVotesArray.Count);
        for (int i = 0; i < allVotes.Length; i++)
        {
            //Debug.Log("Index: " + i);
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        state = maxCount > 1 ? true : false;
        return state;
    }

    public static bool threePlayerGotSameVotes() 
    {
        bool state;
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        if(maxCount == 3)
        {
            state = true;
        }
        else
        {
            state = false;
        }
        return state;
    }
}
