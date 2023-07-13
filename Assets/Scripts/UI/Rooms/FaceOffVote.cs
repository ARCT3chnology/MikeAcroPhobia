using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceOffVote : MonoBehaviour
{
    [SerializeField] Text txt_Answer;
    [SerializeField] Text txt_Votes;
    [SerializeField] Button btn_Vote;
    [SerializeField] FaceOffMenu votingMenu;

    private void Start()
    {
        votingMenu = transform.GetComponentInParent<FaceOffMenu>();
    }


    public void setAnswerTxt(string answer)
    {
        txt_Answer.text = answer;
    }

    public void setVotesTxt(string votes)
    {
        txt_Votes.text = votes;
    }

    public void setVotesTxtGameobjectState(bool state)
    {
        txt_Votes.gameObject.SetActive(state);
    }

    public void setButtonState(bool state)
    {
        btn_Vote.interactable = state;
    }

    public void setButtonGameObjectState(bool state)
    {
        btn_Vote.gameObject.SetActive(state);
    }

    public void setAnswerTextGameObjectState(bool state)
    {
        txt_Answer.gameObject.SetActive(state);
    }
    private ExitGames.Client.Photon.Hashtable _PlayerProperties = new ExitGames.Client.Photon.Hashtable();

    public void OnClick_VoteButton()
    {
        int playerVoteCount = 0;
        int VoteCount = 0;
        AudioManager.Instance.Play("VoteButton");

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != null)
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == txt_Answer.text)
                {
                    if (i == 0)
                    {
                        //Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName);
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 1)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 2)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 3)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 4)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER5_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER5_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 5)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER6_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER6_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 6)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER7_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER7_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu. UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 7)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER8_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER8_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 8)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER9_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER9_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                    if (i == 9)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER10_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER10_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        votingMenu.UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                    }
                }
            }
        }
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
        votingMenu.setVoteButtonInteractableState(false);
    }
}
