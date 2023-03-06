using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyDataHolder : MonoBehaviourPun, IPunObservable
{
    [SerializeField] int _generalLobbyPlayerCount;
    [SerializeField] int _scienceLobbyPlayerCount;
    [SerializeField] int _informationLobbyPlayerCount;
    [SerializeField] int _adultLobbyPlayerCount;
    [SerializeField] LobbyManager _manager;
    public int GeneralLobbyPlayerCount
    {
        get { return _generalLobbyPlayerCount; }
        set { _generalLobbyPlayerCount = value; }
    }
    public int ScienceLobbyPlayerCount
    {
        get { return _scienceLobbyPlayerCount; }
        set { _scienceLobbyPlayerCount = value; }
    }
    public int InformationLobbyPlayerCount
    {
        get { return _informationLobbyPlayerCount; }
        set { _informationLobbyPlayerCount = value; }
    }
    public int AdultLobbyPlayerCount
    {
        get { return _adultLobbyPlayerCount; }
        set { _adultLobbyPlayerCount = value; }
    }


    public void increaseGeneralLobbyCount()
    {

        //photonView.RPC("RPC_PlayerJoinedGeneralLobby", RpcTarget.All);
        RPC_PlayerJoinedGeneralLobby();
    }

    //[PunRPC]
    public void RPC_PlayerJoinedGeneralLobby()
    {
        GeneralLobbyPlayerCount++;
    }
    [PunRPC]
    public void RPC_PlayerJoinedScienceLobby()
    {
        GeneralLobbyPlayerCount++;
    }
    [PunRPC]
    public void RPC_PlayerJoinedInformationlLobby()
    {
        GeneralLobbyPlayerCount++;
    }
    [PunRPC]
    public void RPC_PlayerJoinedAdultLobby()
    {
        GeneralLobbyPlayerCount++;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(GeneralLobbyPlayerCount);
        }
        else
        {
            GeneralLobbyPlayerCount = (int)stream.ReceiveNext();

        }
        //throw new System.NotImplementedException();
    }
}

