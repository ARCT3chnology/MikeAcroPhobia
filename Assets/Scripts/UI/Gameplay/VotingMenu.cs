using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingMenu : MonoBehaviour
{
    [SerializeField] Transform parentObject;
    [SerializeField] GameObject voteUI;
    [SerializeField] List<Vote> voteList;


    private void OnEnable()
    {
        instantiateAnswers();
    }

    public void instantiateAnswers()
    {

        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameObject vote = Instantiate(voteUI, parentObject);
            //Debug.Log(PhotonNetwork.CurrentRoom.Players[i].NickName);
            Vote v = vote.GetComponent<Vote>();
            voteList.Add(v);
            vote.GetComponent<Vote>().setVoteText(PhotonNetwork.PlayerList[i]);
        
        }

    }
}
