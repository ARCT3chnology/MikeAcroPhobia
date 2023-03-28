using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _ThreeLetterRoundPanel;
    [SerializeField] GameObject _WelcomePanel;
    [SerializeField] VotingMenu _VotingPanel;
    [SerializeField] GameObject _EliminatedPanel;
    [SerializeField] GameObject _waitingPanel;
    [SerializeField] RoundConfigurator _RoundConfigurator;
    [SerializeField] GameEndMenu _GameEndMenu;
    [SerializeField] FaceOffMenu _faceOffMenu;
    [SerializeField] GameTieMenu _gameTieMenu;
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    public GameObject welcomePanel { get { return _WelcomePanel; } }
    public GameObject threeLetterRound { get { return _ThreeLetterRoundPanel; } }
    public GameEndMenu gameEndMenu { get { return _GameEndMenu; } }
    public VotingMenu votingPanel { get { return _VotingPanel; } }
    public GameObject eliminatedPanel { get { return _EliminatedPanel; } }
    public GameObject waitingPanel { get { return _waitingPanel; } }
    public RoundConfigurator roundConfigurator { get { return _RoundConfigurator; } }
    public FaceOffMenu faceOffMenu { get { return _faceOffMenu; } }
    public GameTieMenu GameTieMenu { get { return _gameTieMenu; } }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowWelcomePanel", RpcTarget.All);
        }
        GameSettings.normalGame = true;
    }

    public void Start3LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowFirstRoundPanel", RpcTarget.All);
        }
    }
    public void Start4LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowSecondRoundPanel", RpcTarget.All);
        }
    }
    public void Start5LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowThirdRoundPanel", RpcTarget.All);
        }
    }
    public void Start6LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowFourthRoundPanel", RpcTarget.All);
        }
    }    
    public void Start7LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_ShowFifthRoundPanel", RpcTarget.All);
        }
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        OnPlayerVoted(propertiesThatChanged);
    }

    //When ever player votes this function is executed.
    private void OnPlayerVoted(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (propertiesThatChanged.ContainsKey(GameSettings.PlAYERS_VOTED))
        {
            votingPanel.updateVotesStats(4, (int)propertiesThatChanged[GameSettings.PlAYERS_VOTED]);
            if (GameSettings.normalGame)
            {
                if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.TOURNAMENT_NUMBER] == 0)
                {
                    if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == 4)
                    {
                        for (int j = 0; j < votingPanel.voteList.Count; j++)
                        {
                            //Debug.Log("P" + (j + 1).ToString() + "Votes");
                            if (j == 0)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                            }
                            if (j == 1)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                            }
                            if (j == 2)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                            }
                            if (j == 3)
                            {
                                votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                            }
                        }
                        onVotingTimeEnded();
                    }
                }
            else if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.TOURNAMENT_NUMBER] == 1)
            {
                if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == 3)
                {
                    for (int j = 0; j < votingPanel.voteList.Count; j++)
                    {
                        //Debug.Log("P" + (j + 1).ToString() + "Votes");
                        if (j == 0)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                        }
                        if (j == 1)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                        }
                        if (j == 2)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                        }
                        if (j == 3)
                        {
                            votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                        }
                    }
                    onVotingTimeEnded();
                }
            }
            }
            else
            {
                //Both the two players submitted their votes
                if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == 2)
                {
                    for (int i = 0; i < faceOffVoters.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                            {
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i+1].NickName);
                                StartCoroutine(showVotes());
                            }
                        
                        }

                        if(i == 1)
                        {
                            if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                            {
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i-1].NickName);
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                                StartCoroutine(showVotes());
                            }

                        }

                    }
                    for (int i = 0; i < faceOffPlayers.Count; i++)
                    {
                        if (i == 0)
                        {
                            if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                            {
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i+1].NickName);
                                StartCoroutine(showFaceOffAfterVotesWaiting());
                            }
                        
                        }

                        if(i == 1)
                        {
                            if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                            {
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i-1].NickName);
                                //Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                                StartCoroutine(showFaceOffAfterVotesWaiting());
                            }

                        }

                    }
                    if (PhotonNetwork.IsMasterClient)
                    {
                        GameManager.updateFaceOffRoundNumber();
                    }
                    Debug.Log("All two sumbitted their votes");       
                }
            }
        }
    }


    public void DisableFaceoffVoteMenuFromAll()
    {
        Debug.Log("DisableFaceoffVoteMenuFromAll");
        photonView.RPC("RPC_DisableFaceOffVoteMenu", RpcTarget.All);
        resetPlayerVotedCount();
    }

    [PunRPC]
    private void RPC_DisableFaceOffVoteMenu()
    {
        faceOffMenu.DisableVotingOption();
    }


    public IEnumerator showVotes()
    {
        for (int i = 0; i < faceOffVoters.Count; i++)
        {
            if (i == 0)
            {
                if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("showing votes Of: " + faceOffPlayers[i].NickName);
                    Debug.Log("showing votes Of: " + faceOffPlayers[i + 1].NickName);

                    yield return new WaitForSeconds(.1f); 
                    int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
                    int votes2 = (int)faceOffPlayers[i + 1].CustomProperties[GameSettings.PLAYER_VOTES];
                    faceOffMenu.showP1Votes(votes);
                    faceOffMenu.showP2Votes(votes2);
                }

            }

            if (i == 1)
            {
                if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("showing votes Of: " + faceOffPlayers[i - 1].NickName);
                    Debug.Log("showing votes Of: " + faceOffPlayers[i].NickName);

                    yield return new WaitForSeconds(.1f); 
                    int votes1 = (int)faceOffPlayers[i - 1].CustomProperties[GameSettings.PLAYER_VOTES];
                    int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
                    faceOffMenu.showP1Votes(votes1);
                    faceOffMenu.showP2Votes(votes);
                }

            }

        }
    }
    public IEnumerator showFaceOffAfterVotesWaiting()
    {
        for (int i = 0; i < faceOffPlayers.Count; i++)
        {
            if (i == 0)
            {
                if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i + 1].NickName);

                    yield return new WaitForSeconds(.1f);
                    //int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
                    //int votes2 = (int)faceOffPlayers[i + 1].CustomProperties[GameSettings.PLAYER_VOTES];
                    //faceOffMenu.setInfoPanelState(false);
                    faceOffMenu.setInfoPanelText("Please Wait -- All Player voted");
                    //faceOffMenu.showP1Votes(votes);
                    //faceOffMenu.showP2Votes(votes2);
                }

            }

            if (i == 1)
            {
                if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                {
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i - 1].NickName);
                    Debug.Log("Getiing votes Of: " + faceOffPlayers[i].NickName);

                    yield return new WaitForSeconds(.1f);
                    faceOffMenu.setInfoPanelText("Please Wait -- All Player voted");

                    //faceOffMenu.setInfoPanelState(false);
                    //int votes1 = (int)faceOffPlayers[i - 1].CustomProperties[GameSettings.PLAYER_VOTES];
                    //int votes = (int)faceOffPlayers[i].CustomProperties[GameSettings.PLAYER_VOTES];
                    //faceOffMenu.showP1Votes(votes1);
                    //faceOffMenu.showP2Votes(votes);
                }

            }

        }
    }

    public void onVotingTimeEnded()
    {
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GameSettings.PlAYERS_VOTED))
        {
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED] < 4)
            {
                votingPanel.updateVotesStats(4, (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED]);
                votingPanel.hideAllVoteButton();
                for (int j = 0; j < votingPanel.voteList.Count; j++)
                {
                    //Debug.Log("P" + (j + 1).ToString() + "Votes");
                    if (j == 0)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                    }
                    if (j == 1)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                    }
                    if (j == 2)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                    }
                    if (j == 3)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER4_VOTES]);
                    }
                }
            }
        }

        Debug.Log("onVotingTimeEnded");
        if (PhotonNetwork.IsMasterClient && GameManager.getroundNumber() < 5)
        {
            GameManager.updateRoundNumber();
        }
        GameManager.updateAnswersSubmittedNumber(0);
        resetPlayerAnswer();
        Invoke("StartNextRound", 5f);
    }


    /// <summary>
    /// For reseting player answers to null - for the next round.
    /// </summary>
    public void resetPlayerAnswer()
    {
        stats = new ExitGames.Client.Photon.Hashtable();
        stats[GameSettings.PlAYER_ANSWER] = "";
        PhotonNetwork.SetPlayerCustomProperties(stats);
    }

    public void StartNextRound()
    {
        if(threeLetterRound.activeInHierarchy)
        {
            Debug.Log("StartNextRound");        
            threeLetterRound.gameObject.SetActive(false);
            votingPanel.gameObject.SetActive(false);
            votingPanel.voteTimer.StartTime = false;
            votingPanel.resetVotesList();
            resetPlayerVotedCount();
            waitingPanel.SetActive(true);
        }    
    }

    public void resetPlayerVotedCount()
    {

        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, 0 } });
        //PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.NO_OF_ANSWERS_SUBMITTED, 0 } });
    }

    public void GameCompleted()
    {
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        int votes = 0, maxIndex = 0;
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        //checking for same values
        if(allVotes.ToList().Distinct().Count() == 1)
        {
            //it means that all the player contains the same number of votes.
            Debug.Log("all players get same votes.");
            //GameManager.updateRoundNumber(0);
            //Handled in waiting panel script - OnEnable.
        }
        else
        {
            int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
            Debug.Log("Max count is: " + maxCount);
            if (maxCount == 1)
            {
                votes = allVotes.Max();
                maxIndex = allVotes.ToList().IndexOf(votes);
                gameEndMenu.gameObject.SetActive(true);
                Debug.Log("GameCompleted: " + maxIndex.ToString());
                photonView.RPC("RPC_ShowLevelComplete", RpcTarget.All, PhotonNetwork.PlayerList[maxIndex].NickName, votes);
                //gameEndMenu.setEndPanelStats(PhotonNetwork.PlayerList[maxIndex].NickName,votes);
            }
            else if(maxCount == 2)
            {
                if (GameManager.getFaceOffRoundNumber() < 3)
                {
                    FaceOffRounds();
                }
                else
                {
                    //show tie panel.
                    Debug.Log("Game tied");
                    GameTieMenu.gameObject.SetActive(true);
                    GameTieMenu.showPlayers();
                }
            }
            else if (maxCount == 3)
            {
                onthreePlayerGotSameVotes();

            }
        }
    }


    [PunRPC]
    public void RPC_ShowLevelComplete(string nickname, int votes)
    {
        gameEndMenu.setEndPanelStats(nickname, votes);
    }

    public void onthreePlayerGotSameVotes()
    {
        Debug.Log("3 persons got same votes.");
        //remove the one with the lowest score from the game
        //and start new game with the remaining three.
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int minimumValueIndex = allVotes.ToList().IndexOf(allVotes.Min());
        Debug.Log("Player with the lowest vote is: " + PhotonNetwork.PlayerList[minimumValueIndex].NickName);
        if (PhotonNetwork.PlayerList[minimumValueIndex] == PhotonNetwork.LocalPlayer)
        {
            DisconnectPlayer();
            GameSettings.PlayerInRoom = false;
        }
        else
        {

        }

    }


    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene(1);
    }

    //[PunRPC]
    //public void RPC_LeaveRoom()
    //{
    //    Debug.Log("leaving Room");
    //    PhotonNetwork.LeaveLobby();
    //    SceneManager.LoadScene(1);
    //}


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " Leave Room");
        if(GameManager.getroundNumber()==5 && GameManager.threePlayerGotSameVotes())
        {
            if (otherPlayer.NickName != PhotonNetwork.LocalPlayer.NickName)
            {
                Debug.Log("starting next round");
                StartCoroutine(startNextRound(otherPlayer));
            }
            else
            {
                GameSettings.ConnectedtoMaster = false;
                SceneManager.LoadScene(1);
            }
        }
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public IEnumerator startNextRound(Player otherPlayer)
    {
        Debug.Log("Round Starting: "+ GameManager.getroundNumber());
        waitingPanel.GetComponent<WaitingPanel>().SetText(otherPlayer.NickName + " is kicked");
        yield return new WaitForSeconds(3);
        waitingPanel.SetActive(false);
        GameManager.updateRoundNumber(0);
        GameManager.updateTournamentNumber(1);
        yield return new WaitForSeconds(1);
        welcomePanel.SetActive(true);
        //waitingPanel.GetComponent<WaitingPanel>().StartGame();
    }


    public List<Player> faceOffPlayers 
    {
        get;
        set;
    }
    public List<Player> faceOffVoters 
    {
        get;
        set;
    }
    public void FaceOffRounds()
    {
        faceOffPlayers = new List<Player>();
        faceOffVoters = new List<Player>();
        int[] allVotes = new int[PhotonNetwork.CurrentRoom.PlayerCount];
        for (int i = 0; i < allVotes.Length; i++)
        {
            allVotes[i] = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlayerVotesArray[i]];
        }
        int MaxNoOfVotes = allVotes.Max();
        Debug.Log("More than 1 have same votes. Its time to start the face off round.");
        var duplicatesWithIndices = allVotes
        // Associate each name/value with an index
        .Select((Name, Index) => new { Name, Index })
        // Group according to name
        .GroupBy(x => x.Name)
        // Only care about Name -> {Index1, Index2, ..}
        .Select(xg => new
        {
            Name = xg.Key,
            Indices = xg.Select(x => x.Index)
        })
        .OrderByDescending(x => x.Name)
        // And groups with more than one index represent a duplicate key
        .Where(x => x.Indices.Count() > 1);
        foreach (var g in duplicatesWithIndices)
        {

            for (int i = 0; i < g.Indices.ToArray().Count(); i++)
            {
                if (g.Name == allVotes.Max())
                {
                    faceOffPlayers.Add(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                    Debug.Log("FaceOff Players: " + PhotonNetwork.PlayerList[g.Indices.ToArray()[i]].NickName);
                    startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                }
            }
            //Debug.Log("Have duplicate " + g.Name + " with indices " +
            //    string.Join(",", g.Indices.ToArray()));
        }
        for (int i = 0; i < allVotes.Length; i++)
        {
            if (allVotes[i] != MaxNoOfVotes)
            {
                faceOffVoters.Add(PhotonNetwork.PlayerList[i]);
                Debug.Log("FaceOff Voters: " + PhotonNetwork.PlayerList[i].NickName);
                startFaceOffVoter(PhotonNetwork.PlayerList[i]);
            }
        }
    }

    public void restartGame()
    {
        waitingPanel.SetActive(false);
        welcomePanel.SetActive(true);
    }

    public void startFaceOffRound(Player p)
    {
        Debug.Log("Starting face off round");
        GameSettings.FaceOffGame = true;
        GameSettings.normalGame = false;
        //RPC_ShowWaitingPanel();
        photonView.RPC("RPC_ShowWaitingPanel", p);
    }
    public void startFaceOffVoter(Player p)
    {
        GameSettings.FaceOffGame = true;
        GameSettings.normalGame = false;
        photonView.RPC("RPC_ShowWaitingPanel", p);
        //RPC_ShowWaitingPanel();
    }

    public void turnOffTextPanel(Player p)
    {

        photonView.RPC("RPC_TurnOFFTextPanel", p);
    }
    public void turnOffTextPanel( bool startVotingTime)
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.gameObject.SetActive(true);
        if(startVotingTime)
            votingPanel.voteTimer.StartTimer();
        //faceOffMenu.onAnswerSubmission();
        //photonView.RPC("RPC_ShowFaceOffP1Answer", PhotonNetwork.PlayerList[faceOffVoters[0]], (string)PhotonNetwork.PlayerList[faceOffPlayers[0]].CustomProperties[GameSettings.PlAYER_ANSWER]);
        //photonView.RPC("RPC_ShowFaceOffP2Answer", PhotonNetwork.PlayerList[faceOffVoters[0]], (string)PhotonNetwork.PlayerList[faceOffPlayers[1]].CustomProperties[GameSettings.PlAYER_ANSWER]);
        //photonView.RPC("RPC_StartFaceOffVotingTimer", PhotonNetwork.PlayerList[faceOffVoters[0]]);
    }
    public void RPC_OnFaceOffAnswerSubmit(Player p)
    {
        Debug.Log("FaceOff - answer Submit Name: " + p.NickName);
        photonView.RPC("turnOffTextPanelFaceOff_Voter1", p);
        photonView.RPC("turnOffTextPanelFaceOff_Voter2", p);
    }


    [PunRPC]
    private void turnOffTextPanelFaceOff_Voter1()
    {
        faceOffMenu.onAnswerSubmission();
        faceOffMenu.setVoteButtonInteractableState(true);
        faceOffMenu.setVoteButtonState(true);
        photonView.RPC("RPC_ShowFaceOffP1Answer", faceOffVoters[0], (string)faceOffPlayers[0].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_ShowFaceOffP2Answer", faceOffVoters[0], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_StartFaceOffVotingTimer", faceOffVoters[0]);
        faceOffMenu.Vote_Timer.StartTimer();
        //votingPanel.gameObject.SetActive(true);
    }
    [PunRPC]
    private void turnOffTextPanelFaceOff_Voter2()
    {
        faceOffMenu.onAnswerSubmission();
        faceOffMenu.setVoteButtonInteractableState(true);
        faceOffMenu.setVoteButtonState(true);
        photonView.RPC("RPC_ShowFaceOffP1Answer", faceOffVoters[1], (string)faceOffPlayers[0].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_ShowFaceOffP2Answer", faceOffVoters[1], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_StartFaceOffVotingTimer", faceOffVoters[0]);
        //faceOffMenu.Vote_Timer.StartTimer();
        //votingPanel.gameObject.SetActive(true);
    }
    public void makePlayerWaitInFaceOff(Player P)
    {
        photonView.RPC("RPC_MakePlayerWaitinFaceOff", P);
        //RPC_MakePlayerWaitinFaceOff();
    }
    public void makePlayerWaitForFaceOffVoting(Player P)
    {
        photonView.RPC("RPC_MakePlayerWaitinFaceOff", P);
        //RPC_MakePlayerWaitinFaceOff();
    }
    [PunRPC]
    private void RPC_MakePlayerWaitinFaceOff()
    {
        faceOffMenu.showWaitingForVoting();
        faceOffMenu.hidePlayerPanel();
    }


    public void StartFaceOffRounds()
    {
        if (true)
        {
            for (int i = 0; i < faceOffPlayers.Count; i++)
            {
                if (faceOffPlayers[i] == PhotonNetwork.LocalPlayer)
                    photonView.RPC("RPC_faceOffPlayer", faceOffPlayers[i]);
            }

            for (int i = 0; i < faceOffVoters.Count; i++)
            {
                if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                    photonView.RPC("RPC_faceOffVoter", faceOffVoters[i]);
            }
        }

        //faceOffMenu.showPlayerPanel();
        //.showVotersPanel();
    }


    public void updateAnswerOnPlayer(bool playerSubmitted)
    {
        RPC_UpdateAnswersForVoting(playerSubmitted);
    }
    [PunRPC]
    public void RPC_UpdateAnswerOnplayer(Player player, bool answerSubmitted)
    {
        photonView.RPC("RPC_UpdateAnswersForVoting", player, answerSubmitted);
    }
    [PunRPC]
    private void RPC_TurnOFFTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_UpdateAnswersForVoting(bool playerSubmitted)
    {
        //votingPanel.voteTimer.StartTimer();
        votingPanel.updateAnswers(playerSubmitted);
    }
    [PunRPC]
    private void RPC_ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowFirstRoundPanel()
    {
        roundConfigurator.setTitleText("Three letter Round");
        roundConfigurator.setAcronymType(AcronymSetter.acronyms.ThreeLetters);
        roundConfigurator.setTimerForRound(GameSettings.ThreeLetterRoundTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowSecondRoundPanel()
    {
        roundConfigurator.setTitleText("Four letter Round");
        roundConfigurator.setAcronymType(AcronymSetter.acronyms.FourLetters);
        roundConfigurator.setTimerForRound(GameSettings.FourLetterRoundTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);

        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowThirdRoundPanel()
    {
        roundConfigurator.setTitleText("Five letter Round");
        roundConfigurator.setAcronymType(AcronymSetter.acronyms.FiveLetters);
        roundConfigurator.setTimerForRound(GameSettings.FiveLetterRoundTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowFourthRoundPanel()
    {
        roundConfigurator.setTitleText("Six letter Round");
        roundConfigurator.setAcronymType(AcronymSetter.acronyms.SixLetters);
        roundConfigurator.setTimerForRound(GameSettings.SixLetterRoundTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowFifthRoundPanel()
    {
        roundConfigurator.setTitleText("Seven letter Round");
        roundConfigurator.setAcronymType(AcronymSetter.acronyms.SevenLetters);
        roundConfigurator.setTimerForRound(GameSettings.SevenLetterRoundTime);
        roundConfigurator.resetAnswerField();
        threeLetterRound.SetActive(true);
        threeLetterRound.transform.GetChild(0).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(true);
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(true);
    }
    [PunRPC]
    private void RPC_ShowWaitingPanel()
    {
        Debug.Log("Starting face-Off round");
        waitingPanel.SetActive(true);
        waitingPanel.GetComponent<WaitingPanel>().StartGame();
        //faceOffMenu.showPlayerPanel();
    }
    [PunRPC]
    private void RPC_faceOffVoter()
    {
        Debug.Log("Starting face-Off Voter round");
        //waitingPanel.SetActive(true);
        faceOffMenu.showVotersPanel();
    }
    [PunRPC]
    private void RPC_faceOffPlayer()
    {
        Debug.Log("Starting face-Off Player round");
        //waitingPanel.SetActive(true);
        faceOffMenu.showPlayerPanel();
    }
    [PunRPC]
    private void RPC_ShowFaceOffP1Answer(string playerAnswer)
    {
        faceOffMenu.updateP1Answer(playerAnswer);
    }
    [PunRPC]
    private void RPC_ShowFaceOffP2Answer(string playerAnswer)
    {
        faceOffMenu.updateP2Answer(playerAnswer);
    }
    [PunRPC]
    private void RPC_StartFaceOffVotingTimer()
    {
        faceOffMenu.startVoteTimer();
    }

}
