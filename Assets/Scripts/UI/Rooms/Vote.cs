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
    [SerializeField] Text _acroText;

    public Text acroText
    {
        get { return _acroText; }
        set { _acroText = value; }
    }

    [SerializeField] Text nameText;
    [SerializeField] Button votebutton;
    [SerializeField] Text noOfVotes;
    [SerializeField] VotingMenu votingMenu;
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        votingMenu = transform.GetComponentInParent<VotingMenu>();
    }

    private ExitGames.Client.Photon.Hashtable _PlayerProperties = new ExitGames.Client.Photon.Hashtable();
    public void onClick_VoteButton()
    {
        //UnityEngine.Debug.Log("onClick_VoteButton");
        int playerVoteCount;
        int VoteCount;

        AudioManager.Instance.Play("VoteButton");
        for (int i = 0;i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            //UnityEngine.Debug.Log(PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString());
            if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != null)
            {
                if (acroText.text!=null)
                {
                    if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == acroText.text)
                    {
                        if(i == 0)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 1)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 2)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 3)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 4)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER5_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER5_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 5)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER6_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER6_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 6)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER7_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER7_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 7)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER8_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER8_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 8)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER9_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER9_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                        if (i == 9)
                        {
                            playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER10_VOTES];
                            playerVoteCount++;
                            PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER10_VOTES, playerVoteCount } });
                            _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                            PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                            votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        }
                    }

                }
            }
        }
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
        votingMenu.PlayerVoted = true;

        votingMenu.hideAllVoteButton();
    }

    [SerializeField] Color PlayerColor;
    public void setVoteText(Player player)
    {
        acroText.text = (string)player.CustomProperties[GameSettings.PlAYER_ANSWER];
        UnityEngine.Debug.Log("Setting vote text");

        if (player.IsLocal)
        {
            if ((string)player.CustomProperties[GameSettings.PlAYER_ANSWER] == (string)acroText.text)
            {
                votebutton.interactable = false;
                nameText.color = PlayerColor;
            }

        }
        setNameText(player.NickName);
        Invoke(nameof(setVoteStates), 0.5f);
    }

    public void setVoteStates()
    {
        if (votingMenu != null)
        {
            if (votingMenu.PlayerVoted)
            {
                votebutton.interactable = false;
            }

        }
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
        setNameTextState(true);
        PlayerStatsMenu.Instance.UpdateStarsText();
        PlayerStatsMenu.Instance.setExperienceSlider();

    }

    public void setNameText(string name)
    {
        nameText.text = name;
    }

    public void setNameTextState(bool state)
    {
        nameText.gameObject.SetActive(state);
    }
}
