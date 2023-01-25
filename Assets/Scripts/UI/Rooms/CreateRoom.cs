using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEditor;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] Text _roomName;
    private GameCanvas _gamePanel;

    public void firstInitialize(GameCanvas panel)
    {
        _gamePanel = panel;
    }

    public void onClick_CreateRoom()
    {
        if(!PhotonNetwork.IsConnected)
            return;


        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 3;
        options.PlayerTtl = 60;
        options.EmptyRoomTtl = 60;
        
        if(_roomName.text != "")
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully",this);
        _gamePanel.roomPanel.show();
        _gamePanel.roomPanel.UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);

    } 

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed" + message,this);

    }

    
}
