using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _ThreeLetterRoundPanel;
    [SerializeField] GameObject _WelcomePanel;
    [SerializeField] GameObject _VotingPanel;

    [HideInInspector] public GameObject welcomePanel { get { return _WelcomePanel; } }
    [HideInInspector] public GameObject threeLetterRound { get { return _ThreeLetterRoundPanel; } }
    [HideInInspector] public GameObject votingPanel { get { return _VotingPanel; } }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowWelcomePanel", RpcTarget.All);
        }
        //welcomePanel.SetActive(true);
    }

    public void Start3LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowFirstRoundPanel", RpcTarget.All);
        }
        //threeLetterRound.SetActive(true);
    }

    public void turnOffTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.SetActive(true);
    }

    [PunRPC]
    public void RPC_ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }
    [PunRPC]
    public void RPC_ShowFirstRoundPanel()
    {
        threeLetterRound.SetActive(true);
    }
}
