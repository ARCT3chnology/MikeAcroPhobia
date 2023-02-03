using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VotingMenu : MonoBehaviour
{
    [SerializeField] Transform parentObject;
    [SerializeField] GameObject voteUI;
    public List<Vote> voteList;
    [SerializeField] Text voteStats;

    private void OnEnable()
    {
        instantiateAnswers();
        voteStats.text = "0/3 Players Voted";
    }

    public void instantiateAnswers()
    {
        
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            GameObject vote = Instantiate(voteUI, parentObject);
            //Debug.Log(PhotonNetwork.CurrentRoom.Players[i].NickName);
            Vote v = vote.GetComponent<Vote>();
            voteList.Add(v);
            vote.GetComponent<Vote>().setVoteText(PhotonNetwork.PlayerList[i]);
        }

    }

    public void hideAllVoteButton()
    {
        if (voteList!=null)
        {
            for (int i = 0; i < voteList.Count; i++)
            {
                //Debug.Log(PhotonNetwork.PlayerList[i].NickName);
                voteList[i].hideVoteButton(PhotonNetwork.PlayerList[i]);
            }
        }
    }

    public void resetVotesList()
    {
        for (int i = 0; i < voteList.Count; i++)
        {
            Debug.Log("Reseting vote list");    
            Destroy(voteList[i].gameObject);
        }    
        voteList.Clear();
        voteStats.text = "0/3 Players Voted";
    }


    public void updateVotesStats(int maxPlayers, int playerVoted)
    {
        voteStats.text = playerVoted.ToString() + "/" + maxPlayers + "Players Voted";
    }
}
