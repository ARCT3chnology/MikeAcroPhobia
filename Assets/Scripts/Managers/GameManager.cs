using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    [SerializeField] InputField threeLetterAnswerInputField;
    [SerializeField] UiController uiController;
    public void setAnswer()
    {
        _myCustomProperties["ThreeletterAcronym"] = threeLetterAnswerInputField.text;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
        Debug.Log(threeLetterAnswerInputField.text);
    }

    public void OnAnswerTimeComplete()
    {
        uiController.turnOffTextPanel();
    }

}
