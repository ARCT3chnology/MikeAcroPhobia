using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] MultiplayerNetworkManager _mnp;
    [SerializeField] PlayerListing _playerListing;
    [SerializeField] Transform content;

    private List<PlayerListing> _playerLists = new List<PlayerListing>();
    private bool _ready;
    [SerializeField] Text _readyUpText;
    public override void OnEnable()
    {
        base.OnEnable();
        getCurrentRoomplayers();
        SetReadyUp(false);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _playerLists.Count; i++)
        {
            Destroy(_playerLists[i].gameObject);
        }
        _playerLists.Clear();
    }

    #region UI CALLBACKS
    public void ReadyUp_OnClick()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
        }
    }

    public void StartGame_OnClick()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            photonView.RPC("RPC_LoadLevel", PhotonNetwork.PlayerList[i]);
        }
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    //for (int i = 0; i < _playerLists.Count; i++)
        //    //{
        //    //    if (_playerLists[i].Player != PhotonNetwork.LocalPlayer)
        //    //    {
        //    //        if (!_playerLists[i].Ready)
        //    //            return;
        //    //    }
        //    //}
        //}

    }

    [PunRPC]
    public void RPC_LoadLevel()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        }
        else
        {
#if UNITY_EDITOR
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
#endif
            Debug.Log("All Players not connected");
        }
    }

    #endregion

    #region PRIVATE FUNCTIONS
    private void addPlayerlisting(Player player)
    {
        Debug.Log("addPlayerlisting");
        int index = _playerLists.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _playerLists[index].setPlayerInfo(player);
        }
        else
        {
            Debug.Log("instantiating player list");
            PlayerListing listing = (PlayerListing)Instantiate(_playerListing, content);
            if (listing != null)
            {
                listing.setPlayerInfo(player);
                _playerLists.Add(listing);
            }

        }
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
        {
            _readyUpText.text = "R";
        }
        else
        {
            _readyUpText.text = "N";

        }
    }
    private void getCurrentRoomplayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            addPlayerlisting(playerInfo.Value);
        }
    }

    #endregion

    #region PHOTON CALLBACKS
    //PLAYER CALLBACKS
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _mnp.LeaveRoom_OnClick();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        addPlayerlisting(newPlayer);
        photonView.RPC("RPC_ChangePlayerCount", RpcTarget.All);
        if(PhotonNetwork.CurrentRoom.PlayerCount ==3)
        {
            StartGame_OnClick();
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _playerLists.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_playerLists[index].gameObject);
            _playerLists.RemoveAt(index);
        }
    }

    //PUN RPC CALLBACKS
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _playerLists.FindIndex(x => x.Player == player);
        if (index != -1)
            _playerLists[index].Ready = ready;
    }

    [PunRPC]
    private void RPC_ChangePlayerCount()
    {
        _mnp.UpdatePlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers, PhotonNetwork.CurrentRoom.PlayerCount);
        _mnp.SetPlayerCoundAndRoomLogic();
    }

    #endregion
}
