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

    
    public void setAnswer()
    {
        if (GameSettings.normalGame)
        {
            stats = new ExitGames.Client.Photon.Hashtable();
            stats[GameSettings.PlAYER_ANSWER] = normalGameInputField.text.ToString();
            Debug.Log("Text is: " + normalGameInputField.text);
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
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        //uiController.updateAnswerOnPlayer(targetPlayer);
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
    public static void updateFaceOffRoundNumber()
    {
        int roundNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.FACEOFF_ROUND_NUMBER];
        roundNumber++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.FACEOFF_ROUND_NUMBER, roundNumber } });
    }

    public void OnAnswerTimeComplete()
    {
        if (GameSettings.normalGame)
        {
            //for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            //{
            //    uiController.updateAnswerOnPlayer(PhotonNetwork.PlayerList[i]);
            //}
            uiController.turnOffTextPanel();
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
            uiController.updateAnswerOnPlayer();

            uiController.turnOffTextPanel();
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
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
        state = maxCount > 1 ? true : false;
        return state;
    }
}
