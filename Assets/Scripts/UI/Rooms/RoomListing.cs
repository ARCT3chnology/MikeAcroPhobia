using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour
{
    [SerializeField] Text _text;

    public RoomInfo _roomInfo { get; private set; }

    public void setRoomInfo(RoomInfo roomInfo)
    {
        _roomInfo = roomInfo;
        _text.text = roomInfo.PlayerCount.ToString()+" / "+roomInfo.MaxPlayers.ToString()+ ", " + roomInfo.Name;
    }

    public void onCLick_Button()
    {
        PhotonNetwork.JoinRoom(_roomInfo.Name);    
    }
}
