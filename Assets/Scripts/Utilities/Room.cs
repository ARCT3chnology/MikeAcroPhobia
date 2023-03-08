using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Room : MonoBehaviour
{
    [SerializeField] TMP_Text Txt_RoomName;
    [SerializeField] TMP_Text Txt_PlayersCount;
    
    public void setRoomStats(string roomName,int PlayerCount)
    {
        Txt_RoomName.text = roomName;
        Txt_PlayersCount.text = PlayerCount.ToString() + "/" + 4 + "Players joined.";
    }
}
