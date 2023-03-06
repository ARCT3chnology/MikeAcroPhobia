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
    private string mapType;

    [Header("-----PLAYER COUNT TEXT-----")]
    public Text generalText;
    public Text adultText;
    public Text scienceText;
    public Text informationText;

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
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 3;
        options.PlayerTtl = 60;
        options.EmptyRoomTtl = 60;
        addRoomProperties(options);
        PhotonNetwork.CreateRoom("General", options, TypedLobby.Default);
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
    }


    public override void OnJoinedLobby()
    {
        //base.OnJoinedLobby();

        if (PhotonNetwork.CurrentLobby.IsDefault)
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 3;
            options.PlayerTtl = 60;
            options.EmptyRoomTtl = 60;
            
            addRoomProperties(options);
            PhotonNetwork.CreateRoom("General",options,TypedLobby.Default);
            //PhotonNetwork.CreateRoom("Adult",options);
            //PhotonNetwork.CreateRoom("Science",options);
            //PhotonNetwork.CreateRoom("Information",options);
        }
        Debug.Log("Master cleint Joined lobby");
        if (PhotonNetwork.IsMasterClient)
        {
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

        //MenuManager.Instance.OpenMenu(menuName.Room); //OEPN ROOM
        
        //UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, PhotonNetwork.CurrentRoom.MaxPlayers);

        //SetPlayerCoundAndRoomLogic();

    }


    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room creation failed" + message, this);

    }

    public override void OnJoinedRoom()
    {
        //MenuManager.Instance.OpenMenu(menuName.Room);
        UpdatePlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers, PhotonNetwork.CurrentRoom.PlayerCount);
        content.DestroyChildren();
        _rooms.Clear();

        //HUZIAFA
        //CHECKING WHICH ROOM HAS BEEN JOINED BY WHICH PLAYER
        Debug.Log("The Local player: " + PhotonNetwork.NickName + " joined to " + PhotonNetwork.CurrentRoom.Name
                    + " Player count " + PhotonNetwork.CurrentRoom.PlayerCount);

        //if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MAP_TYPE_KEY))
        //{
        //    object mapType;
        //    if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MAP_TYPE_KEY, out mapType))
        //    {
        //        Debug.Log("Joined room with the map: " + (string)mapType);
        //        if ((string)mapType == RoomName.General.ToString())
        //        {
        //            //DO ANYTHING AFTER ROOM JOIN
        //            Debug.Log(RoomName.General.ToString());
        //            UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.General);

        //        }
        //        else if ((string)mapType == RoomName.Adult.ToString())
        //        {
        //            //DO ANYTHING AFTER ROOM JOIN
        //            Debug.Log(RoomName.Adult.ToString());
        //            UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Adult);

        //        }
        //        else if ((string)mapType == RoomName.Science.ToString())
        //        {
        //            //DO ANYTHING AFTER ROOM JOIN
        //            Debug.Log(RoomName.Science.ToString());
        //            UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Science);

        //        }
        //        else if ((string)mapType == RoomName.Information.ToString())
        //        {
        //            //DO ANYTHING AFTER ROOM JOIN
        //            Debug.Log(RoomName.Information.ToString());
        //            UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Information);

        //        }

        //    }
        //}

        SetPlayerCoundAndRoomLogic();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        //if (roomList.Count == 0)
        //{
        //    //There is no room at all
        //    generalText.text = 0 + " / " + 3;
        //    adultText.text = 0 + " / " + 3;
        //    scienceText.text = 0 + " / " + 3;
        //    informationText.text = 0 + " / " + 3;
        //    Debug.Log("resetting text");
        //}
        Debug.Log(roomList.Count);
        foreach (RoomInfo info in roomList)
        {
            Debug.Log("Room list updated" + info.RemovedFromList);
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
                Debug.Log("Room list updated" + index);
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

            //HUZAIFA CODE
            //CHECKING & SETTING PLAYER COUNTS
            //CheckRoomNameAndSetCountText(info);

            //if (info.Name.Contains(RoomName.General.ToString()))
            //{
            //    Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //    generalText.text = info.PlayerCount + " / " + 3;

            //    #region text changer

            //    if (info.Name.Contains(RoomName.Adult.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        adultText.text = info.PlayerCount + " / " + 3;

            //    }
            //    else if (info.Name.Contains(RoomName.Science.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        scienceText.text = info.PlayerCount + " / " + 3;

            //    }
            //    else if (info.Name.Contains(RoomName.Information.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        informationText.text = info.PlayerCount + " / " + 3;

            //    }
            //    #endregion
            //}
            //else if (info.Name.Contains(RoomName.Adult.ToString()))
            //{
            //    Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //    adultText.text = info.PlayerCount + " / " + 3;

            //    #region text changer

            //    if (info.Name.Contains(RoomName.General.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        generalText.text = info.PlayerCount + " / " + 3;
            //    }
            //    else if (info.Name.Contains(RoomName.Science.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        scienceText.text = info.PlayerCount + " / " + 3;

            //    }
            //    else if (info.Name.Contains(RoomName.Information.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        informationText.text = info.PlayerCount + " / " + 3;

            //    }
            //    #endregion
            //}
            //else if (info.Name.Contains(RoomName.Science.ToString()))
            //{
            //    Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //    scienceText.text = info.PlayerCount + " / " + 3;

            //    #region test changer
            //    if (info.Name.Contains(RoomName.General.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        generalText.text = info.PlayerCount + " / " + 3;
            //    }
            //    else if (info.Name.Contains(RoomName.Adult.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        adultText.text = info.PlayerCount + " / " + 3;

            //    }
            //    else if (info.Name.Contains(RoomName.Information.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        informationText.text = info.PlayerCount + " / " + 3;

            //    }
            //    #endregion
            //}
            //else if (info.Name.Contains(RoomName.Information.ToString()))
            //{
            //    Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //    informationText.text = info.PlayerCount + " / " + 3;

            //    #region test cahnger
            //    if (info.Name.Contains(RoomName.General.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        generalText.text = info.PlayerCount + " / " + 3;

            //    }
            //    else if (info.Name.Contains(RoomName.Adult.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        adultText.text = info.PlayerCount + " / " + 3;
            //    }
            //    else if (info.Name.Contains(RoomName.Science.ToString()))
            //    {
            //        Debug.Log("Room is a" + info.Name + " Player count is: " + info.PlayerCount);
            //        scienceText.text = info.PlayerCount + " / " + 3;
            //    }
            //    #endregion
            //}
            //else { Debug.Log("No Rooms Found"); }
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        CreateAndJoinRoom();
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

    //HUZAIFA
    //NEW ROOM CREATION FUNCTION
    //THIS FUNCTION IS USED ON ROOMS BUTTON CLICK
    public const string MAP_TYPE_KEY = "map";
    public void OnEnterButton_OnClick(int number)
    {
        RoomName a = (RoomName)number;
        mapType = a.ToString();
        Debug.Log("MAP NAME " + mapType);

        //ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
        //{
        //    { MAP_TYPE_KEY, mapType },
        //    //{ GameSettings.PlAYER1_VOTES, 0},
        //    //{ GameSettings.PlAYER2_VOTES, 0 },
        //    //{ GameSettings.PlAYER3_VOTES, 0 },
        //    //{ GameSettings.PlAYERS_VOTED, 0 },
        //    //{ GameSettings.ROUND_NUMBER, 0 },
        //    //{ GameSettings.FACEOFF_ROUND_NUMBER, 0 }
        //};
        Hashtable expectedCustomRoomProperties = new Hashtable();
        expectedCustomRoomProperties.Add(GameSettings.PlAYER1_VOTES, 0);
        expectedCustomRoomProperties.Add(GameSettings.PlAYER2_VOTES, 0);
        expectedCustomRoomProperties.Add(GameSettings.PlAYER3_VOTES, 0);
        expectedCustomRoomProperties.Add(GameSettings.PlAYER4_VOTES, 0);
        expectedCustomRoomProperties.Add(GameSettings.PlAYERS_VOTED, 0);
        expectedCustomRoomProperties.Add(GameSettings.ROUND_NUMBER, 0);
        expectedCustomRoomProperties.Add(GameSettings.FACEOFF_ROUND_NUMBER, 0);
        expectedCustomRoomProperties.Add(MAP_TYPE_KEY, mapType);
        PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 3);
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
    //HUZAIFA 
    //NEW ROOM TEXT UPDATE FUNCTION
    public void UpdatePlayerCount(int CurrentPlayer, RoomName name)
    {
        switch (name)
        {
            case RoomName.General:
                Debug.Log("CHANGING ROOM TEXT " + name);
                if (generalText) generalText.text = CurrentPlayer + " / " + 3;
                break;
            case RoomName.Adult:
                Debug.Log("CHANGING ROOM TEXT " + name);
                if (adultText) adultText.text = CurrentPlayer + " / " + 3;
                break;
            case RoomName.Science:
                Debug.Log("CHANGING ROOM TEXT " + name);
                if (scienceText) scienceText.text = CurrentPlayer + " / " + 3;
                break;
            case RoomName.Information:
                Debug.Log("CHANGING ROOM TEXT " + name);
                if (informationText) informationText.text = CurrentPlayer + " / " + 3;
                break;
            default:
                break;
        }
    }


    public void SetPlayerCoundAndRoomLogic()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(MAP_TYPE_KEY))
        {
            object mapType;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(MAP_TYPE_KEY, out mapType))
            {
                Debug.Log("Joined room with the map: " + (string)mapType);
                if ((string)mapType == RoomName.General.ToString())
                {
                    //DO ANYTHING AFTER ROOM JOIN
                    Debug.Log(RoomName.General.ToString());
                    UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.General);

                }
                else if ((string)mapType == RoomName.Adult.ToString())
                {
                    //DO ANYTHING AFTER ROOM JOIN
                    Debug.Log(RoomName.Adult.ToString());
                    UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Adult);

                }
                else if ((string)mapType == RoomName.Science.ToString())
                {
                    //DO ANYTHING AFTER ROOM JOIN
                    Debug.Log(RoomName.Science.ToString());
                    UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Science);

                }
                else if ((string)mapType == RoomName.Information.ToString())
                {
                    //DO ANYTHING AFTER ROOM JOIN
                    Debug.Log(RoomName.Information.ToString());
                    UpdatePlayerCount(PhotonNetwork.CurrentRoom.PlayerCount, RoomName.Information);

                }

            }
        }
    }
    #endregion

    #region PRIVATE FUNCTIONS
    private void CheckRoomNameAndSetCountText(RoomInfo info)
    {
        if (info.Name.Contains(RoomName.General.ToString()))
        {
            Debug.Log("Room is a" + name + " Player count is: " + info.PlayerCount);
            generalText.text = info.PlayerCount + " / " + 3;
        }
        else if (info.Name.Contains(RoomName.Adult.ToString()))
        {
            Debug.Log("Room is a" + name + " Player count is: " + info.PlayerCount);
            adultText.text = info.PlayerCount + " / " + 3;

        }
        else if (info.Name.Contains(RoomName.Science.ToString()))
        {
            Debug.Log("Room is a" + name + " Player count is: " + info.PlayerCount);
            scienceText.text = info.PlayerCount + " / " + 3;

        }
        else if (info.Name.Contains(RoomName.Information.ToString()))
        {
            Debug.Log("Room is a" + name + " Player count is: " + info.PlayerCount);
            informationText.text = info.PlayerCount + " / " + 3;

        }
        else { Debug.Log("No Rooms Found"); }
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
    private void CreateAndJoinRoom()
    {
        string randomRoomName = "Room_" + mapType + Random.Range(0, 10000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;


        string[] roomPropsInLobby = { MAP_TYPE_KEY };

        ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { MAP_TYPE_KEY, mapType } };

        roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
        roomOptions.CustomRoomProperties = customRoomProperties;

        PhotonNetwork.CreateRoom(randomRoomName, roomOptions);

    }
    #endregion




}
