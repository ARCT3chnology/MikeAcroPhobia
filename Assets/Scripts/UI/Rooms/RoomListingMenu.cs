using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListingMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] RoomListing _roomListing;
    [SerializeField] Transform content;

    private List<RoomListing> _rooms = new List<RoomListing>();
    private GameCanvas _gamePanel;
    public void FirstInitialize(GameCanvas gamePanel)
    {
        _gamePanel = gamePanel;
    }

    public override void OnJoinedRoom()
    {
        _gamePanel.roomPanel.show();
        _gamePanel.roomPanel.UpdatePlayerCount(PhotonNetwork.CurrentRoom.MaxPlayers,PhotonNetwork.CurrentRoom.PlayerCount);
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
                    if(listing!=null)
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


}
