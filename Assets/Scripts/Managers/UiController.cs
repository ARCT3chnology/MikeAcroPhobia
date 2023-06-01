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
            photonView.RPC( nameof(RPC_ShowWelcomePanel), RpcTarget.All);
        }
        AudioManager.Instance.Play("Welcome");
        AudioManager.Instance.Play("Gameplay");
        GameSettings.normalGame = true;
    }

    public void Start3LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowFirstRoundPanel), RpcTarget.All);
        }
    }
    public void Start4LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowSecondRoundPanel), RpcTarget.All);
        }
    }
    public void Start5LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowThirdRoundPanel), RpcTarget.All);
        }
    }
    public void Start6LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowFourthRoundPanel), RpcTarget.All);
        }
    }    
    public void Start7LetterRound()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(RPC_ShowFifthRoundPanel), RpcTarget.All);
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
                    if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == PhotonNetwork.CurrentRoom.PlayerCount)
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
                        votingPanel.voteTimer.gameObject.SetActive(false);
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
                    //if (PhotonNetwork.IsMasterClient)
                    //{
                    //    GameManager.updateFaceOffRoundNumber();
                    //}
                    if (!GameManager.faceOffRoundNumberIncreased)
                    {
                        GameManager.faceOffRoundNumber++;
                        GameManager.faceOffRoundNumberIncreased = true;
                    }
                    Debug.Log("All two sumbitted their votes");       
                }
            }
        }
    }


    public void DisableFaceoffVoteMenuFromAll()
    {
        Debug.Log("DisableFaceoffVoteMenuFromAll");
        photonView.RPC(nameof(RPC_DisableFaceOffVoteMenu), RpcTarget.All);
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
        Debug.Log("onVotingTimeEnded");
        if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey(GameSettings.PlAYERS_VOTED))
        {
            if ((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED] < PhotonNetwork.CurrentRoom.PlayerCount)
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
        if (PhotonNetwork.IsMasterClient && GameManager.getroundNumber() < 5)
        {
            GameManager.updateRoundNumber();
        }
        GameManager.updateAnswersSubmittedNumber(0);
        resetPlayerAnswer();
        Invoke(nameof(StartNextRound), 5f);
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
                AudioManager.Instance.Stop("Gameplay");
                votes = allVotes.Max();
                maxIndex = allVotes.ToList().IndexOf(votes);
                //gameEndMenu.StartTimer();
                PhotonNetwork.AutomaticallySyncScene = false;
                photonView.RPC(nameof(RPC_ShowLevelComplete), RpcTarget.All, PhotonNetwork.PlayerList[maxIndex].NickName, votes);
                Debug.Log("GameCompleted: " + maxIndex.ToString());
                GameSettings.PlayerInRoom = false;
                if (GameSettings.CurrentRooms != null)
                {
                    foreach (var item in GameSettings.CurrentRooms)
                    {
                        if(item.roomName == PhotonNetwork.CurrentRoom.Name)
                            item.playerCount = 0;
                    }
                }

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
                    GameTieMenu.showPlayers();
                    GameSettings.PlayerInRoom = false;
                    if (GameSettings.CurrentRooms != null)
                    {
                        foreach (var item in GameSettings.CurrentRooms)
                        {
                            if (item.roomName == PhotonNetwork.CurrentRoom.Name)
                                item.playerCount = 0;
                        }
                    }
                }
            }
            else if (maxCount == 3)
            {
                onthreePlayerGotSameVotes();

            }
        }
    }

    [PunRPC]
    public void RPC_GameCompleted() 
    {
        GameCompleted();
    }

    [PunRPC]
    public void RPC_ShowLevelComplete(string nickname, int votes)
    {
        gameEndMenu.gameObject.SetActive(true);
        gameEndMenu.setEndPanelStats(nickname, votes);
        //PhotonNetwork.Disconnect();
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
            if (PhotonNetwork.IsMasterClient)
            {
                DisconnectPlayer();
            }
            GameSettings.PlayerInRoom = false;
        }
        else
        {

        }

    }

    public void DisconnectPlayer()
    {
        AudioManager.Instance.Play("MenuButton");
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.CloseConnection(PhotonNetwork.LocalPlayer);
        if(PhotonNetwork.NetworkClientState != ClientState.Disconnected)
        {
            PhotonNetwork.LeaveRoom();
            while (PhotonNetwork.InRoom)
            {
                yield return null;
            }
            AudioManager.Instance.Stop("Gameplay");
            PhotonNetwork.Disconnect(); // Disconnect from Photon network
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    
    }

    public void loadLobby()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if (PhotonNetwork.PlayerList[i]!= PhotonNetwork.MasterClient)
            {
                photonView.RPC(nameof(RPC_LeaveRoom), RpcTarget.All, PhotonNetwork.PlayerList[i]);
            }
                //RPC_LeaveRoom(PhotonNetwork.PlayerList[i]);
        }
        if(PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(DisconnectAndLoad());
        }
        //photonView.RPC(nameof(RPC_LeaveRoom), RpcTarget.All);
    }

    

    [PunRPC]
    public void RPC_LeaveRoom(Player p)
    {
        PhotonNetwork.CloseConnection(p);
        SceneManager.LoadScene(1);
        Debug.Log("leaving Room");
        //PhotonNetwork.LeaveRoom();
        //SceneManager.LoadScene(1);
    }

    //public int playerleft;
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //playerleft = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_LEFT];
        //playerleft++;
        //PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_LEFT,playerleft} });
        Debug.Log(otherPlayer.NickName + " Left the Room");
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
        if(PhotonNetwork.CurrentRoom.PlayerCount == 0)
            PhotonNetwork.CurrentRoom.IsOpen = true;
        //if(PhotonNetwork.LocalPlayer == otherPlayer)
        //{
        //    SceneManager.LoadScene(1);
        //}
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
        photonView.RPC(nameof(RPC_ShowWaitingPanel), p);
    }
    public void startFaceOffVoter(Player p)
    {
        GameSettings.FaceOffGame = true;
        GameSettings.normalGame = false;
        photonView.RPC(nameof(RPC_ShowWaitingPanel), p);
        //RPC_ShowWaitingPanel();
    }

    public void turnOffTextPanel(Player p)
    {
        photonView.RPC(nameof(RPC_TurnOFFTextPanel), p);
    }

    public void turnOffTextPanel( bool startVotingTime)
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        //threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
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
        photonView.RPC(nameof(turnOffTextPanelFaceOff_Voter1), p);
        photonView.RPC(nameof(turnOffTextPanelFaceOff_Voter2), p);
    }


    [PunRPC]
    private void turnOffTextPanelFaceOff_Voter1()
    {
        faceOffMenu.onAnswerSubmission();
        faceOffMenu.setVoteButtonInteractableState(true);
        faceOffMenu.setVoteButtonState(true);
        photonView.RPC(nameof(RPC_ShowFaceOffP1Answer), faceOffVoters[0], (string)faceOffPlayers[0].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC(nameof(RPC_ShowFaceOffP2Answer), faceOffVoters[0], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC(nameof(RPC_StartFaceOffVotingTimer), faceOffVoters[0]);
        faceOffMenu.Vote_Timer.StartTimer();
        //votingPanel.gameObject.SetActive(true);
    }
    [PunRPC]
    private void turnOffTextPanelFaceOff_Voter2()
    {
        faceOffMenu.onAnswerSubmission();
        faceOffMenu.setVoteButtonInteractableState(true);
        faceOffMenu.setVoteButtonState(true);
        photonView.RPC(nameof(RPC_ShowFaceOffP1Answer), faceOffVoters[1], (string)faceOffPlayers[0].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC(nameof(RPC_ShowFaceOffP2Answer), faceOffVoters[1], (string)faceOffPlayers[1].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC(nameof(RPC_StartFaceOffVotingTimer), faceOffVoters[1]);
        faceOffMenu.Vote_Timer.StartTimer();
        //votingPanel.gameObject.SetActive(true);
    }
    public void makePlayerWaitInFaceOff(Player P)
    {
        photonView.RPC(nameof(RPC_MakePlayerWaitinFaceOff), P);
        //RPC_MakePlayerWaitinFaceOff();
    }
    public void makePlayerWaitForFaceOffVoting(Player P)
    {
        photonView.RPC(nameof(RPC_MakePlayerWaitinFaceOff), P);
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
                {
                    photonView.RPC(nameof(RPC_faceOffPlayer), faceOffPlayers[i]);

                }
            }

            for (int i = 0; i < faceOffVoters.Count; i++)
            {
                if (faceOffVoters[i] == PhotonNetwork.LocalPlayer)
                {
                    photonView.RPC(nameof(RPC_faceOffVoter), faceOffVoters[i]);
                }
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
        photonView.RPC(nameof(RPC_UpdateAnswersForVoting), player, answerSubmitted);
    }
    [PunRPC]
    private void RPC_TurnOFFTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        //threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
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

    [PunRPC]
    private void RPC_UpdateStars()
    {
        PlayerStats.CurrentStars++;
        //PlayerStatsMenu.Instance.setExperienceSlider();
    }

    public void updateStars(Player targetPlayer)
    {
        photonView.RPC(nameof(RPC_UpdateStars), targetPlayer);
    }


}
