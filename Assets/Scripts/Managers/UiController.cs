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
    private ExitGames.Client.Photon.Hashtable stats = new ExitGames.Client.Photon.Hashtable();

    public GameObject welcomePanel { get { return _WelcomePanel; } }
    public GameObject threeLetterRound { get { return _ThreeLetterRoundPanel; } }
    public GameEndMenu gameEndMenu { get { return _GameEndMenu; } }
    public VotingMenu votingPanel { get { return _VotingPanel; } }
    public GameObject eliminatedPanel { get { return _EliminatedPanel; } }
    public GameObject waitingPanel { get { return _waitingPanel; } }
    public RoundConfigurator roundConfigurator { get { return _RoundConfigurator; } }
    public FaceOffMenu faceOffMenu { get { return _faceOffMenu; } }

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
        resetPlayerAnswer();
        Invoke("StartNextRound", 6f);
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
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.NO_OF_ANSWERS_SUBMITTED, 0 } });
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
                gameEndMenu.setEndPanelStats(PhotonNetwork.PlayerList[maxIndex].NickName,votes);
            }
            else if(maxCount == 2)
            {
                FaceOffRounds();
            }
            else if (maxCount == 3)
            {
                Debug.Log("3 persons got same votes.");
                //remove the one with the lowest score from the game
                //and start new game with the remaining three.
                int minimumValueIndex = allVotes.ToList().IndexOf(allVotes.Min());
                Debug.Log("Player with the lowest vote is: " + PhotonNetwork.PlayerList[minimumValueIndex].NickName);
                if (PhotonNetwork.PlayerList[minimumValueIndex] == PhotonNetwork.LocalPlayer)
                {
                    DisconnectPlayer();
                    GameSettings.PlayerInRoom = false;
                }
            }
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

    [PunRPC]
    public void RPC_LeaveRoom()
    {
        Debug.Log("leaving Room");
        PhotonNetwork.LeaveLobby();
        SceneManager.LoadScene(1);
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + " Leave Room");
        if (otherPlayer.NickName == PhotonNetwork.LocalPlayer.NickName)
            SceneManager.LoadScene(1);
        base.OnPlayerLeftRoom(otherPlayer);
    }


    private List<int> faceOffPlayers;
    private List<int> faceOffVoters;
    public void FaceOffRounds()
    {
        faceOffPlayers = new List<int>();
        faceOffVoters = new List<int>();
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
                faceOffPlayers.Add(g.Indices.ToArray()[i]);
                startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
            }
            //Debug.Log("Have duplicate " + g.Name + " with indices " +
            //    string.Join(",", g.Indices.ToArray()));
        }
        for (int i = 0; i < allVotes.Length; i++)
        {
            if (allVotes[i] != MaxNoOfVotes)
            {
                startFaceOffVoter(PhotonNetwork.PlayerList[i]);
                faceOffVoters.Add(i);
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
        photonView.RPC("RPC_faceOffRound", p);
    }
    public void startFaceOffVoter(Player p)
    {
        photonView.RPC("RPC_faceOffVoter", p);
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
    
    public void turnOffTextPanelFaceOff()
    {
        faceOffMenu.onAnswerSubmission();
        photonView.RPC("RPC_ShowFaceOffP1Answer", PhotonNetwork.PlayerList[faceOffVoters[0]], (string)PhotonNetwork.PlayerList[faceOffPlayers[0]].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_ShowFaceOffP2Answer", PhotonNetwork.PlayerList[faceOffVoters[0]], (string)PhotonNetwork.PlayerList[faceOffPlayers[1]].CustomProperties[GameSettings.PlAYER_ANSWER]);
        photonView.RPC("RPC_StartFaceOffVotingTimer", PhotonNetwork.PlayerList[faceOffVoters[0]]);
        //votingPanel.gameObject.SetActive(true);
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
    public void RPC_TurnOFFTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.gameObject.SetActive(true);
    }

    [PunRPC]
    public void RPC_UpdateAnswersForVoting(bool playerSubmitted)
    {
        //votingPanel.voteTimer.StartTimer();
        votingPanel.updateAnswers(playerSubmitted);
    }

    [PunRPC]
    public void RPC_ShowWelcomePanel()
    {
        welcomePanel.SetActive(true);
    }
    [PunRPC]
    public void RPC_ShowFirstRoundPanel()
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
    public void RPC_ShowSecondRoundPanel()
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
    public void RPC_ShowThirdRoundPanel()
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
    public void RPC_ShowFourthRoundPanel()
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
    public void RPC_ShowFifthRoundPanel()
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
    public void RPC_faceOffRound()
    {
        Debug.Log("Starting face-Off round");
        waitingPanel.SetActive(false);
        faceOffMenu.showPlayerPanel();
    }
    [PunRPC]
    public void RPC_faceOffVoter()
    {
        Debug.Log("Starting face-Off Voter round");
        waitingPanel.SetActive(false);
        faceOffMenu.showVotersPanel();
    }
    [PunRPC]
    public void RPC_ShowFaceOffP1Answer(string playerAnswer)
    {
        faceOffMenu.updateP1Answer(playerAnswer);
    }
    [PunRPC]
    public void RPC_ShowFaceOffP2Answer(string playerAnswer)
    {
        faceOffMenu.updateP2Answer(playerAnswer);
    }
    [PunRPC]
    public void RPC_StartFaceOffVotingTimer()
    {
        faceOffMenu.startVoteTimer();
    }

}
