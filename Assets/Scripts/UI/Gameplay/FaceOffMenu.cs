using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FaceOffMenu : MonoBehaviour
{
    [SerializeField] GameObject PlayerPanel;
    [SerializeField] GameObject VoterPanel;
    [SerializeField] GameObject waitingPanel;
    [SerializeField] Timer VoteTimer;
    [SerializeField] Text P1Answer;
    [SerializeField] Text P2Answer;
    [SerializeField] Button[] VoteButtons;

    public void showPlayerPanel()
    {
        this.gameObject.SetActive(true);
        PlayerPanel.SetActive(true);
    }

    public void showVotersPanel()
    {
        this.gameObject.SetActive(true);
        VoterPanel.SetActive(true);
    }

    public void onAnswerSubmission()
    {
        PlayerPanel.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        PlayerPanel.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        PlayerPanel.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        PlayerPanel.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);
    }
    public void updateP1Answer(string p1)
    {
        P1Answer.text = p1;
    }

    public void updateP2Answer(string p2)
    {
        P2Answer.text = p2;
    }

    public void startVoteTimer()
    {
        VoteTimer.StartTime = true;
    }
    public void DisableVotingOption()
    {
        setVoteButtonState(false);
        GameManager.updateFaceOffRoundNumber();
        //waitingPanel.SetActive(true);
    }

    public void OnClick_VoteButtonP1()
    {
        int playerVoteCount = 0;
        int VoteCount = 0;
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != "")
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == P1Answer.text)
                {
                    if (i == 0)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                    }
                    if (i == 1)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                    }
                    if (i == 2)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                    }
                    if (i == 3)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                    }
                }

            }
        }
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
        setVoteButtonState(false);
    }

    public void OnClick_VoteButtonP2()
    {
        int playerVoteCount = 0;
        int VoteCount = 0;
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != "")
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == P2Answer.text)
                {
                    if (i == 0)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                    }
                    if (i == 1)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                    }
                    if (i == 2)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                    }
                    if (i == 3)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                    }
                }

            }
        }
        setVoteButtonState(false);

        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
    }

    public void setVoteButtonState(bool state)
    {
        for (int i = 0; i < VoteButtons.Length; i++)
        {
            VoteButtons[i].interactable = state;
        }
    }
}
