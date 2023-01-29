using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vote : MonoBehaviour
{
    [SerializeField] Text answerText;
    [SerializeField] Button votebutton;
    [SerializeField] Text noOfVotes;
    [SerializeField] VotingMenu votingMenu;
    private void Start()
    {
        votingMenu = transform.root.GetComponent<VotingMenu>();
    }

    public void onClick_VoteButton()
    {

    }


    public void setVoteText(Player player )
    {
        //if(PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("P13Letters"))
        answerText.text = (string)player.CustomProperties["3Letter"];
    }
}
