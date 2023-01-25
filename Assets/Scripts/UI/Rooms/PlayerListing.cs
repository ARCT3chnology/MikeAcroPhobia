using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{
    [SerializeField] Text _text;

    public Player Player { get; private set; }
    public bool Ready;
    public void setPlayerInfo(Player player)
    {
        Player = player;
        //int result = -1;
        //if(player.CustomProperties.ContainsKey("RandomNumber"))
        //    result = (int)player.CustomProperties["RandomNumber"];
        _text.text = /*result.ToString() + ", " + */player.NickName;
    }
}
