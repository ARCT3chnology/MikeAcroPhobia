using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Linq.Expressions;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public struct CategoryPanel
    {
        public TMP_Text Txt_Count;
    }

    [SerializeField] CategoryPanel _generalCategory;
    public CategoryPanel GeneralCategoryPanel 
    {
        get { return _generalCategory; }
        set { }
    }

    [SerializeField] CategoryPanel _scienceCategory;
    public CategoryPanel ScienceCategoryPanel
    {
        get { return _scienceCategory; }
        set { }
    }

    [SerializeField] CategoryPanel _informationCategory;
    public CategoryPanel InformationCategoryPanel
    {
        get { return _informationCategory; }
        set { }
    }
    
    [SerializeField] CategoryPanel _adultCategory;
    public CategoryPanel AdultCategoryPanel
    {
        get { return _adultCategory; }
        set { }
    }

    private TypedLobby GeneralLobby = new TypedLobby("General", LobbyType.Default);
    private TypedLobby ScienceLobby = new TypedLobby("Science", LobbyType.Default);
    private TypedLobby InformationLobby = new TypedLobby("Information", LobbyType.Default);
    private TypedLobby AdultLobby = new TypedLobby("Adult", LobbyType.Default);

    public bool generalRoomFull { get; set; }
    public bool scienceRoomFull { get; set; }
    public bool informationRoomFull { get; set; }
    public bool adultRoomFull { get; set; }

    [SerializeField] Room _Room;
    public Room Room { get { return _Room; }set { } }

    [SerializeField] GameObject _roomFillPanel;
    public GameObject roomFillPanel
    {
        get
        {
            return _roomFillPanel;
        }
        set 
        { 
            _roomFillPanel = 
                value;
        }
    }
    [SerializeField] GameObject _lobbyPanel;
    public GameObject LobbyPanel
    {
        get
        {
            return _lobbyPanel;
        }
        set 
        {
            _lobbyPanel = 
                value;
        }
    }

    public enum Categories 
    {
        General = 0,
        Science = 1,
        Information = 2,
        Adult = 3,
    }

    public Categories LobbyCategory;


    /// <summary>
    /// this fucntion is called on each category button in lobby system scene-lobbies panel.
    /// </summary>
    public void OnClick_CategoryButton(int Category)
    {
        Debug.Log(PhotonNetwork.InLobby);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        switch ((Categories)Category)
        {
            case Categories.General:
                {
                    if (PhotonNetwork.InLobby)
                    {
                        RoomOptions options = new RoomOptions();
                        options.MaxPlayers = 4;
                        options.PlayerTtl = 0;
                        options.EmptyRoomTtl = 0;
                        options.IsOpen = true;
                        options.IsVisible = true;
                    
                        addRoomProperties(options);
                        Debug.Log("General Room is full: " +generalRoomFull);
                        if (!generalRoomFull)
                        {
                            PhotonNetwork.JoinOrCreateRoom("General", options, TypedLobby.Default);
                        }
                        else
                        {
                            roomFillPanel.SetActive(true);
                        }
                    }
                    break;
                }
            case Categories.Science:
                if (PhotonNetwork.InLobby)
                {
                    //PhotonNetwork.JoinLobby(ScienceLobby);
                }
                break;
            case Categories.Information:
                if (PhotonNetwork.InLobby)
                {
                    //PhotonNetwork.JoinLobby(InformationLobby);
                }
                break;
            case Categories.Adult:
                if (PhotonNetwork.InLobby)
                {
                    //PhotonNetwork.JoinLobby(AdultLobby);
                }
                break;
            default:
                break;
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
    }

    public override void OnJoinedLobby()
    {
        Debug.Log(PhotonNetwork.CurrentLobby.Name + " is joined by player: " + GameSettings.NickName);
        if (GameSettings.CurrentRooms!=null)
        {
            UpdateUi(GameSettings.CurrentRooms);
        }
        
        base.OnJoinedLobby();
    
    }

    public override void OnLeftLobby()
    {
        Debug.Log(GameSettings.NickName + " Left Lobby");
        base.OnLeftLobby();
    }

    public override void OnJoinedRoom() 
    {
        Debug.Log("Room Joined of category: " + PhotonNetwork.CurrentRoom.Name);
        MenuManager.Instance.OpenMenu(menuName.RoomPanel);
        GameSettings.PlayerInRoom = true;
        //Room.setRoomStats(PhotonNetwork.CurrentRoom.Name, PhotonNetwork.CurrentRoom.PlayerCount);

        if (PhotonNetwork.CurrentRoom.PlayerCount == 4)
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                photonView.RPC("RPC_LoadLevel", PhotonNetwork.PlayerList[i]);
            }
      
            //PhotonNetwork.LoadLevel(2);  
        }
        else
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                photonView.RPC("RPC_UpdatePlayerCount", PhotonNetwork.PlayerList[i]);
            }
        }

    }

    public override void OnLeftRoom()
    {
        Debug.Log(GameSettings.NickName + " Left Room");
        UpdateUi(GameSettings.CurrentRooms);
        LobbyPanel.SetActive(true);
        Room.gameObject.SetActive(false);
        //photonView.RPC("RPC_UpdatePlayerCount", RpcTarget.All);
           
        //base.OnLeftRoom();
        //if (!PhotonNetwork.InLobby)
        //{
        //}
        SceneManager.LoadScene(0);
    }

    [PunRPC]
    public void RPC_UpdatePlayerCount()
    {
        if (PhotonNetwork.CurrentRoom!=null)
        {
            Room.setRoomStats(PhotonNetwork.CurrentRoom.Name, PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
    [PunRPC]
    public void RPC_LoadLevel()
    {
        Debug.Log("Loading level in Lobby system");
        SceneManager.LoadScene(2);
    }

    private static void addRoomProperties(RoomOptions options)
    {

        Hashtable roomProps = new Hashtable();
        roomProps.Add(GameSettings.PlAYER1_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER2_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER3_VOTES, 0);
        roomProps.Add(GameSettings.PlAYER4_VOTES, 0);
        roomProps.Add(GameSettings.PlAYERS_VOTED, 0);
        roomProps.Add(GameSettings.ROUND_NUMBER, 0);
        roomProps.Add(GameSettings.FACEOFF_ROUND_NUMBER, 0);
        roomProps.Add(GameSettings.ALL_ANSWERS_SUBMITTED, false);
        roomProps.Add(GameSettings.NO_OF_ANSWERS_SUBMITTED, 0);
        options.CustomRoomProperties = roomProps;
    }

    private void Start()
    {
        
        LobbyPanel.SetActive(true);
        Debug.Log("Client is connected to master: " + GameSettings.ConnectedtoMaster);
        PhotonNetwork.AutomaticallySyncScene = true;
        
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.EnableCloseConnection = true;
        }
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        //UpdateUi(GameSettings.CurrentRooms);
        //This function will call onRoomListUpdate if the getcustomRoomList is true.
        //setPlayerCount();
    }

    public override void OnConnectedToMaster()
    {
        UpdateUi(GameSettings.CurrentRooms);
        //base.OnConnectedToMaster();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnR0omListUpdate is called " + roomList.Count);

        foreach (RoomInfo room in roomList)
        {
            if(room.Name == "General")
            {
                if(room.PlayerCount == 4)
                    generalRoomFull = true;
            }
        }

        GameSettings.CurrentRooms = roomList;
        UpdateUi(roomList);
        base.OnRoomListUpdate(roomList);
    }

    private void UpdateUi(List<RoomInfo> roomList)
    {
        if (roomList!=null)
        {
            if (roomList.Count > 0)
        {
            foreach (var item in roomList)
            {
                Debug.Log("Room Name: " + item.Name);
                switch (item.Name)
                {
                    case "General":
                        {
                            GeneralCategoryPanel.Txt_Count.text = item.PlayerCount.ToString() + "/" + 4;

                            break;
                        }
                    case "Science":
                        {
                            ScienceCategoryPanel.Txt_Count.text = item.PlayerCount.ToString() + "/" + 4;

                            break;
                        }
                    case "Information":
                        {
                            InformationCategoryPanel.Txt_Count.text = item.PlayerCount.ToString() + "/" + 4;

                            break;
                        }
                    case "Adult":
                        {
                            AdultCategoryPanel.Txt_Count.text = item.PlayerCount.ToString() + "/" + 4;

                            break;
                        }
                }

            }
        }

        }
    }

    public void onClick_onBackButton()
    {
        SceneManager.LoadScene(0);
    }

    public void onClick_LeaveRoomButton()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    } 

    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        foreach (var item in GameSettings.CurrentRooms)
        {
            switch (item.Name)
            {
                case "General":
                    {
                        if (item.PlayerCount < 4)
                        {
                            generalRoomFull = false;
                        }
                        break;
                    }
                case "Science":
                    {
                        if (item.PlayerCount < 4)
                        {
                            scienceRoomFull= false;
                        }
                        break;
                    }                
                case "Information":
                    {
                        if (item.PlayerCount < 4)
                        {
                            informationRoomFull = false;
                        }
                        break;
                    }
                case "Adult":
                    {
                        if (item.PlayerCount < 4)
                        {
                            adultRoomFull = false;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
        
        base.OnRoomPropertiesUpdate(propertiesThatChanged);
    }

    public void setPlayerCount()
    {
        //PhotonNetwork.GetCustomRoomList(GeneralLobby, "C0");

    }
}
