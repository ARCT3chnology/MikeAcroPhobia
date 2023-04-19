using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class VotingMenu : MonoBehaviour
{
    [SerializeField] Transform parentObject;
    [SerializeField] GameObject voteUI;
    public List<Vote> voteList;
    [SerializeField] Text voteStats;
    public bool submitPressed { get; set; }
    [SerializeField] VoteTimer _voteTimer;
    public bool PlayerVoted;
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    public VoteTimer voteTimer
    {
        get { return _voteTimer; }
        set { _voteTimer = value; } 
    }

    private void OnEnable()
    {
        if (!submitPressed)
        {
            Debug.Log("Instantiating from enable");
            instantiateAnswers(false);
            submitPressed = false;
        }
        voteStats.text = "0/4 Players Voted";
    }

    private void OnDisable()
    {
        PlayerVoted = false;
        if (PhotonNetwork.LocalPlayer.IsLocal)
        {
            stats = new ExitGames.Client.Photon.Hashtable();
            stats[GameSettings.ANSWER_SUBMITTED] = false;
            PhotonNetwork.SetPlayerCustomProperties(stats);
        }
    }


    public void instantiateAnswers(bool playerSubmitted)
    {
        //Debug.Log("instantiateAnswers");
        if (voteList.Count > 0)
        {
            foreach (var item in voteList)
            {
                //Debug.Log("Destroying: " + item.ToString());
                DestroyImmediate(item.gameObject);
            }
        }
        voteList.Clear();
        //Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        if (!playerSubmitted)
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                //Debug.Log("Answer is: " + PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER]);
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != null)
                {
                    GameObject vote = Instantiate(voteUI, parentObject);
                    //Debug.Log(PhotonNetwork.PlayerList[i].NickName);
                    Vote v = vote.GetComponent<Vote>();
                    voteList.Add(v);
                    vote.GetComponent<Vote>().setVoteText(PhotonNetwork.PlayerList[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.ANSWER_SUBMITTED] != null)
                {
                    //Debug.Log("Answer is: " + PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER]);
                    if ((bool)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.ANSWER_SUBMITTED] == true)
                    {
                        GameObject vote = Instantiate(voteUI, parentObject);
                        //Debug.Log(PhotonNetwork.PlayerList[i].NickName);
                        Vote v = vote.GetComponent<Vote>();
                        voteList.Add(v);
                        vote.GetComponent<Vote>().setVoteText(PhotonNetwork.PlayerList[i]);
                    }

                }
            }
        }

        //voteTimer.StartTimer();
    }

    public void updateAnswers(bool playerSubmitted)
    {
        //Debug.Log("updateAnswers");

        instantiateAnswers(playerSubmitted);
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
            //Debug.Log("Reseting vote list");    
            Destroy(voteList[i].gameObject);
        }    
        voteList.Clear();
        voteStats.text = "0/4 Players Voted";
    }


    public void updateVotesStats(int maxPlayers, int playerVoted)
    {
        voteStats.text = playerVoted.ToString() + "/" + maxPlayers + "Players Voted";
    }
}
