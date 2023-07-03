using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceOffMenu : MonoBehaviour
{
    [SerializeField] GameObject PlayerPanel;
    [SerializeField] GameObject VoterPanel;
    [SerializeField] GameObject ProgressPanel;
    [SerializeField] InfoPanel inforPanel;
    [SerializeField] Timer VoteTimer;
    [SerializeField] Timer _AnswerTimer;
    [SerializeField] Text P1Answer;
    [SerializeField] Text P2Answer;
    [SerializeField] Text LevelNamePlayer;
    [SerializeField] Text LevelNameVoter;
    [SerializeField] Button[] VoteButtons;
    [SerializeField] Text[] Votes_Txt;
    [SerializeField] InputField P1TextInput;
    [SerializeField] Button submitButton;
    public Timer Vote_Timer
    {
        get 
        {
            return VoteTimer;
        }
        set 
        { 
            VoteTimer = value;
        }
    }
    public Timer Answer_Timer
    {
        get 
        {
            return _AnswerTimer;
        }
        set 
        { 
            VoteTimer = value;
        }
    }

    [SerializeField] UiController _uiController;
    public UiController UIController
    {
        get
        {
            return _uiController;
        }
        set
        {
            UIController = _uiController;
        }
    }

    public void showWaiting()
    {
        inforPanel.setinfoText("Please Wait");
        inforPanel.gameObject.SetActive(true);
        Debug.Log("ShowWaiting");
    }
    public void showWaitingForVoting()
    {
        inforPanel.setinfoText("Please Wait -- Voting In Progress");
        inforPanel.gameObject.SetActive(true);
        Debug.Log("ShowWaiting");
    }

    public void setInfoPanelState(bool state) 
    {
        inforPanel.gameObject.SetActive(state);
    }
    public void setInfoPanelText(string text) 
    {
        inforPanel.setinfoText(text);
    }

    public void showPlayerPanel()
    {
        ProgressPanel.GetComponent<WaitingPanel>().resetTimer();
        this.gameObject.SetActive(true);
        PlayerPanel.SetActive(true);
        P1TextInput.SetTextWithoutNotify("");
        submitButton.interactable = true;
        LevelNamePlayer.text = "FaceOff Round: " + (GameManager.faceOffRoundNumber + 1).ToString();
        ProgressPanel.SetActive(false);
    }

    public void hidePlayerPanel()
    {
        PlayerPanel.SetActive(false);
        P1TextInput.SetTextWithoutNotify("");
        submitButton.interactable = false;
    }

    public void showVotersPanel()
    {
        ProgressPanel.GetComponent<WaitingPanel>().resetTimer();
        this.gameObject.SetActive(true);
        VoterPanel.SetActive(true);
        setVoteButtonInteractableState(false);
        LevelNameVoter.text = "FaceOff Round: " + (GameManager.faceOffRoundNumber+1).ToString();
        setVoteButtonState(true);
        setAnsterText("");
        ProgressPanel.SetActive(false);
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
        Debug.Log("Disable Voting Option");
        setVoteButtonInteractableState(false);
        VoteTimer.resetTimer();
        ProgressPanel.SetActive(true);
        inforPanel.gameObject.SetActive(false);

    }

    private ExitGames.Client.Photon.Hashtable _PlayerProperties1 = new ExitGames.Client.Photon.Hashtable();

    public void OnClick_VoteButtonP1()
    {
        int playerVoteCount = 0;
        int VoteCount = 0;
        AudioManager.Instance.Play("VoteButton");

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != "")
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == P1Answer.text)
                {
                    if (i == 0)
                    {
                        //Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName);
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties1;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties);
                    }
                    if (i == 1)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties1;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties1);
                    }
                    if (i == 2)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties1;
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties1);
                    }
                    if (i == 3)
                    {
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                        _PlayerProperties[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties1;
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties1);
                    }
                }
            }
        }
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
        setVoteButtonInteractableState(false);
    }
    private ExitGames.Client.Photon.Hashtable _PlayerProperties = new ExitGames.Client.Photon.Hashtable();
    public void OnClick_VoteButtonP2()
    {
        int playerVoteCount = 0;
        int VoteCount = 0;
        AudioManager.Instance.Play("VoteButton");

        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if ((string)PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER] != "")
            {
                if (PhotonNetwork.PlayerList[i].CustomProperties[GameSettings.PlAYER_ANSWER].ToString() == P2Answer.text)
                {
                    if (i == 0)
                    {

                        //Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName);
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER1_VOTES, playerVoteCount } });
                        _PlayerProperties1[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties1);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties;

                    }
                    if (i == 1)
                    {

                        //Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName);
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER2_VOTES, playerVoteCount } });
                        _PlayerProperties1[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties;
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties1);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);

                    }
                    if (i == 2)
                    {

                        //Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName);
                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER3_VOTES, playerVoteCount } });
                        _PlayerProperties1[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties;
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties1);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);

                    }
                    if (i == 3)
                    {

                        playerVoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES];
                        playerVoteCount++;
                        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYER4_VOTES, playerVoteCount } });
                        _PlayerProperties1[GameSettings.PLAYER_VOTES] = playerVoteCount;
                        Debug.Log("Vote added to: " + PhotonNetwork.PlayerList[i].NickName + "Votes" + playerVoteCount);
                        //PhotonNetwork.SetPlayerCustomProperties(_PlayerProperties);
                        //PhotonNetwork.PlayerList[i].CustomProperties = _PlayerProperties;
                        PhotonNetwork.PlayerList[i].SetCustomProperties(_PlayerProperties1);
                        UpdateStarOfSpecficPlayer(PhotonNetwork.PlayerList[i]);

                    }
                }

            }
        }
        setVoteButtonInteractableState(false);
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount++;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
    }

    public void setVoteButtonInteractableState(bool state)
    {
        for (int i = 0; i < VoteButtons.Length; i++)
        {
            VoteButtons[i].interactable = state;
            //VoteButtons[i].gameObject.SetActive(!state);
        }
    }
    public void setVoteButtonState(bool state)
    {
        for (int i = 0; i < VoteButtons.Length; i++)
        {
            //VoteButtons[i].interactable = state;
            VoteButtons[i].gameObject.SetActive(state);
        }
    }

    public void setAnsterText(string text)
    {
        Debug.Log("setAnswerText: " + text);
        P1Answer.text = text;
        P2Answer.text = text;
        Votes_Txt[0].gameObject.SetActive(false);
        Votes_Txt[1].gameObject.SetActive(false);

    }

    public void showP1Votes(int votes)
    {
        Debug.Log("Showing P1 Votes: " + votes);
        Votes_Txt[0].text = votes.ToString();
        Votes_Txt[0].gameObject.SetActive(true);
        VoteButtons[0].gameObject.SetActive(false);
    }
    public void showP2Votes(int votes)
    {
        Debug.Log("Showing P2 Votes: " + votes);
        Votes_Txt[1].text = votes.ToString();
        Votes_Txt[1].gameObject.SetActive(true);
        VoteButtons[1].gameObject.SetActive(false);

    }
    public void UpdateStarOfSpecficPlayer(Player targetPlayer)
    {
        UIController.updateStars(targetPlayer);
    }
}
