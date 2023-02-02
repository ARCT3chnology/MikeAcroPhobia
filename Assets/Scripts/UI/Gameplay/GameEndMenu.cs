using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text VotesText;

    public void setEndPanelStats(string name, int votes)
    {
        NameText.text = name;
        VotesText.text = votes.ToString();
    }

    public void onClick_ContinueButton()
    {
        PhotonNetwork.LeaveRoom(true);
        PhotonNetwork.LoadLevel(1);
    }
}
