using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDetails : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text VotesText;

    public void setText(Player player)
    {
        NameText.text = player.NickName;
        if (player.CustomProperties[GameSettings.PLAYER_VOTES] != null)
        {
            VotesText.text = player.CustomProperties[GameSettings.PLAYER_VOTES].ToString();
        }
    }
}
