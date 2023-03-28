using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTieMenu : MonoBehaviour
{

    [SerializeField] Transform ContentParent;
    [SerializeField] GameObject PlayerDetail;
    

    public void showPlayers()
    {
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
            GameObject playerdetails = Instantiate(PlayerDetail, ContentParent);
            PlayerDetails pd = playerdetails.GetComponent<PlayerDetails>();
            pd.setText(PhotonNetwork.PlayerList[i]);
        }
    }
}
