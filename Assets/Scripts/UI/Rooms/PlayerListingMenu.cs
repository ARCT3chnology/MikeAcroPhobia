using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] PlayerListing _playerListing;
    [SerializeField] Transform content;

    private List<PlayerListing> _playerLists = new List<PlayerListing>();
    private GameCanvas _gameCanvas;
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
    public void FirstInitialize(GameCanvas panel)
    {
        _gameCanvas = panel;
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

    private void addPlayerlisting(Player player)
    {
        int index = _playerLists.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _playerLists[index].setPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = (PlayerListing)Instantiate(_playerListing, content);
            if (listing != null)
            {
                listing.setPlayerInfo(player);
                _playerLists.Add(listing);
            }

        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        _gameCanvas.roomPanel.leaveRoomMenu.onClick_LeaveRoom();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        addPlayerlisting(newPlayer);
        photonView.RPC("RPC_ChangePlayerCount", RpcTarget.All);
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

    public void onClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _playerLists.Count; i++)
            {
                if (_playerLists[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (!_playerLists[i].Ready)
                        return;
                }
            }
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                PhotonNetwork.LoadLevel(1);
            }else
            {
                Debug.Log("All Players not connected");
            }
        }
    }

    public void onClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            photonView.RPC("RPC_ChangeReadyState",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer,_ready);
        }
    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player,bool ready)
    {
        int index = _playerLists.FindIndex(x => x.Player == player);
        if (index != -1)
            _playerLists[index].Ready = ready;
    }

    [PunRPC]
    private void RPC_ChangePlayerCount()
    {
        _gameCanvas.roomPanel.UpdatePlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers, PhotonNetwork.CurrentRoom.PlayerCount);
    }
}
