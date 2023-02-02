using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class MultiplayerNetworkManager : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable _myCustomProperties = new ExitGames.Client.Photon.Hashtable();
    private List<RoomListing> _rooms = new List<RoomListing>();
    

    [Header("-----UI References-----")]
    [SerializeField] InputField nameInput;
    [SerializeField] Text _roomName;
    [SerializeField] Text _textPlayercount;
    [SerializeField] Text _textBtn;
    [SerializeField] Transform content;


    [Header("-----Prefabs-----")]
    [SerializeField] RoomListing _roomListing;




    public static MultiplayerNetworkManager instance;

    #region UNITY CALLBACKS
    private void Awake()
    {
        if (instance != null) instance = this;
    }
    private void Start()
    {

        Debug.Log("Connecting to server");
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void OnApplicationQuit()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LeaveRoom();
        }
    }


    #endregion

    #region PHOTON CALL BACKS

    //CONNECTING TO MASTER
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " connected to master");
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();

        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server, CAUSE: " + cause.ToString());
    }

    //ROOMS CALLBACKS
    public override void OnCreatedRoom()
    {
        Debug.Log("Created room successfully", this);
        MenuManager.Instance.OpenMenu(menuName.Room); //OEPN ROOM
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);

    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed" + message, this);

    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(menuName.Room);
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers, PhotonNetwork.CurrentRoom.PlayerCount);
        content.DestroyChildren();
        _rooms.Clear();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Room list updated");
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _rooms.FindIndex(x => x._roomInfo.Name == info.Name);
                if (index != -1)
                {

                    Destroy(_rooms[index].gameObject);
                    _rooms.RemoveAt(index);
                }
            }
            else
            {
                int index = _rooms.FindIndex(x => x._roomInfo.Name == info.Name);
                if (index == -1)
                {
                    RoomListing listing = (RoomListing)Instantiate(_roomListing, content);
                    if (listing != null)
                    {
                        listing.setRoomInfo(info);
                        _rooms.Add(listing);
                    }
                }
                else
                {
                    _rooms[index].setRoomInfo(info);
                    Debug.Log("Index is: " + index);
                }
            }
        }
    }


    #endregion

    #region UI CALLBACKS

    public void SettingNickName_OnClick()
    {
        if (nameInput != null)
        {
            PhotonNetwork.NickName = nameInput.text;
            MenuManager.Instance.OpenMenu(menuName.CreateRoomPanel);
        }
    }

    public void CreateRoom_OnClick()
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

    public void Button_OnClick()
    {
        setCustomNumber();
    }

    public void LeaveRoom_OnClick()
    {
        PhotonNetwork.LeaveRoom(true);
        MenuManager.Instance.OpenMenu(menuName.CreateRoomPanel);
    }


    #endregion

    #region  PUBLIC CALL BACKS
    public void UpdatePlayerCount(int totalPlayer, int CurrentPlayer)
    {
        _textPlayercount.text = CurrentPlayer.ToString() + " / " + totalPlayer.ToString();
    }
    #endregion

    #region PRIVATE FUNCTIONS
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

    private void setCustomNumber()
    {
        System.Random rnd = new System.Random();
        int result = rnd.Next(0, 99);
        _textBtn.text = result.ToString();
        _myCustomProperties["RandomNumber"] = result;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
    }

    #endregion




}
