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
        //if (_myCustomProperties.ContainsKey("ThreeletterAcronym"))
        //{
        //    //_myCustomProperties.Add("ThreeletterAcronym",threeLetterAnswerInputField.text.ToString());
        //    _myCustomProperties["ThreeletterAcronym"] = threeLetterAnswerInputField.text.ToString();

        //}
        //else
        //{
        //}

        stats = new ExitGames.Client.Photon.Hashtable();
        stats["3Letter"] = threeLetterAnswerInputField.text.ToString();
        PhotonNetwork.SetPlayerCustomProperties(stats);
        //Debug.Log("setting answer");
        //Debug.Log(PhotonNetwork.PlayerList[0].NickName + "setting answer");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    }

    public void OnAnswerTimeComplete()
    {
        uiController.turnOffTextPanel();
    }

}
