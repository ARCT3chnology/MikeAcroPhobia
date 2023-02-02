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
    [SerializeField] InputField threeLetterAnswerInputField;
    [SerializeField] UiController uiController;

    
    public void setAnswer()
    {
        stats = new ExitGames.Client.Photon.Hashtable();
        stats[GameSettings.PlAYER_ANSWER] = threeLetterAnswerInputField.text.ToString();
        PhotonNetwork.SetPlayerCustomProperties(stats);
        //Debug.Log("setting answer");
        //Debug.Log(PhotonNetwork.PlayerList[0].NickName + "setting answer");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    //public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    //{
    //    uiController.votingPanel.updateVotesStats(3, (int)propertiesThatChanged["PlayerVoted"]);
    //}

    public static int getroundNumber()
    {
        return (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ROUND_NUMBER];
    }

    public static void updateRoundNumber()
    {
        int roundNumber = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.ROUND_NUMBER];
        roundNumber++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.ROUND_NUMBER, roundNumber } });
    }

    public void OnAnswerTimeComplete()
    {
        uiController.turnOffTextPanel();
    }

}
