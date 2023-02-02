using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using UnityEditor;
using Hashtable = ExitGames.Client.Photon.Hashtable;

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
        if (!PhotonNetwork.IsConnected)
            return;


        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 3;
        options.PlayerTtl = 60;
        options.EmptyRoomTtl = 60;
        addRoomProperties(options);

        if (_roomName.text != "")
            PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    private static void addRoomProperties(RoomOptions options)
    {
        Hashtable roomProps = new Hashtable();
        roomProps.Add(GameSettings.PlAYER1_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER2_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER3_VOTES, 0);
        roomProps.Add(GameSettings.PlAYERS_VOTED, 0);
        roomProps.Add(GameSettings.ROUND_NUMBER, 0);
        options.CustomRoomProperties = roomProps;
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
