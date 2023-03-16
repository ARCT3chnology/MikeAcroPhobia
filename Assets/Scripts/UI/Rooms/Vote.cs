using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Vote : MonoBehaviour
{
    [SerializeField] Text answerText;
    [SerializeField] Button votebutton;
    [SerializeField] Text noOfVotes;
    [SerializeField] VotingMenu votingMenu;
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        votingMenu = transform.GetComponentInParent<VotingMenu>();
    }

    public void onClick_VoteButton()
    {
        //UnityEngine.Debug.Log("onClick_VoteButton");
        int playerVoteCount;
        int VoteCount;
        for (int i = 0;i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            //UnityEngine.Debug.Log(PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString());
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != "")
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == answerText.text)
                {
                    if(i == 0)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;    
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                        //UnityEngine.Debug.Log("P1 Voted " + PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                    }
                    if(i == 1)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                        //PhotonNetwork.CurrentRoom.CustomProperties["P2Votes"] =  playerVoteCount;
                        //UnityEngine.Debug.Log("P2 Voted");
                    }
                    if(i == 2)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                        //PhotonNetwork.CurrentRoom.CustomProperties["P3Votes"]= playerVoteCount;
                        //UnityEngine.Debug.Log("P3 Voted");
                    }
                    if(i == 3)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                        //PhotonNetwork.CurrentRoom.CustomProperties["P3Votes"]= playerVoteCount;
                        //UnityEngine.Debug.Log("P3 Voted");
                    }
                }
            }
        }
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
        votingMenu.hideAllVoteButton();
    }


    public void setVoteText(Player player)
    {
        answerText.text = (string)player.CustomProperties[GameSettings.PlAYER_ANSWER];
        //UnityEngine.Debug.Log(PhotonNetwork.LocalPlayer.IsLocal);

        //For testing purpose commenting these 82-87
        //if (player.IsLocal)
        //{
        //    if ((string)player.CustomProperties[GameSettings.PlAYER_ANSWER] == (string)answerText.text)
        //        votebutton.interactable = false;
        //}

    }

    /// <summary>
    /// hiding votes buttons and showing counts of votes.
    /// </summary>
    /// <param name="player"></param>
    public void hideVoteButton(Player player)
    {
        votebutton.interactable = false;
    }

    public void showVotes(int votes)
    {
        votebutton.gameObject.SetActive(false);
        noOfVotes.gameObject.SetActive(true);
        noOfVotes.text = votes.ToString();
    }

}
