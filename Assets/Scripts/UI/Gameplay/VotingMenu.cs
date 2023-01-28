using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingMenu : MonoBehaviour
{
    [SerializeField] Transform parentObject;
    [SerializeField] GameObject voteUI;

    private void OnEnable()
    {
        instantiateAnswers();
    }

    public void instantiateAnswers()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameObject vote = Instantiate(voteUI, parentObject);
            vote.GetComponent<Vote>().setVoteText(PhotonNetwork.CurrentRoom.Players[i]);
        }
    }
}
