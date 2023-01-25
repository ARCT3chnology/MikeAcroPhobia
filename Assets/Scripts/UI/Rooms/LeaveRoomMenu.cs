using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveRoomMenu : MonoBehaviour
{
    private GameCanvas _gamePanel;
    public void FirstInitialize(GameCanvas gamePanel)
    {
        _gamePanel = gamePanel;
    }

    public void onClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        _gamePanel.roomPanel.hide();
    }
}
