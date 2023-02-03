using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
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
        GameSettings.gameStarted = true;
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
        if (propertiesThatChanged.ContainsKey(GameSettings.PlAYERS_VOTED))
        {
            votingPanel.updateVotesStats(3, (int)propertiesThatChanged[GameSettings.PlAYERS_VOTED]);
            if ((int)propertiesThatChanged[GameSettings.PlAYERS_VOTED] == 3)
            {
                for (int j = 0; j < votingPanel.voteList.Count; j++)
                {
                    //Debug.Log("P" + (j + 1).ToString() + "Votes");
                    if(j == 0)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER1_VOTES]);
                    }
                    if(j == 1)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER2_VOTES]);
                    }
                    if(j == 2)
                    {
                        votingPanel.voteList[j].showVotes((int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYER3_VOTES]);
                    }
                }
                GameManager.updateRoundNumber();
                Invoke("enableVotingpanel", 3f);

            }

        }
    }

    public void enableVotingpanel()
    {
        if(threeLetterRound.activeInHierarchy)
        {
            threeLetterRound.gameObject.SetActive(false);
            votingPanel.gameObject.SetActive(false);
            votingPanel.resetVotesList();
            resetPlayerVotedCount();
            waitingPanel.SetActive(true);
        }    
    }

    public void resetPlayerVotedCount()
    {
        int VoteCount;
        VoteCount = (int)PhotonNetwork.CurrentRoom.CustomProperties[GameSettings.PlAYERS_VOTED];
        VoteCount = 0;
        PhotonNetwork.CurrentRoom.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { GameSettings.PlAYERS_VOTED, VoteCount } });
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
        }
        else
        {
            int maxCount = allVotes.ToList().Where(x => x == allVotes.Max()).Count();
            if (maxCount == 1)
            {
                votes = allVotes.Max();
                maxIndex = allVotes.ToList().IndexOf(votes);
                gameEndMenu.gameObject.SetActive(true);
                Debug.Log("GameCompleted: " + maxIndex.ToString());
                gameEndMenu.setEndPanelStats(PhotonNetwork.PlayerList[maxIndex].NickName,votes);
            }
            else if(maxCount > 1)
            {
                int MaxNoOfVotes = allVotes.Max();
                Debug.Log("More than 1 have same votes. Its time to start the face off round.");
                var duplicatesWithIndices = allVotes
                // Associate each name/value with an index
                .Select((Name, Index) => new { Name, Index })
                // Group according to name
                .GroupBy(x => x.Name)
                // Only care about Name -> {Index1, Index2, ..}
                .Select(xg => new {
                Name = xg.Key,
                Indices = xg.Select(x => x.Index)
                })
                .OrderByDescending(x=>x.Name)
                // And groups with more than one index represent a duplicate key
                .Where(x => x.Indices.Count() > 1);

                foreach (var g in duplicatesWithIndices)
                {

                    for (int i = 0; i < g.Indices.ToArray().Count(); i++)
                    {
                        startFaceOffRound(PhotonNetwork.PlayerList[g.Indices.ToArray()[i]]);
                    }
                    //Debug.Log("Have duplicate " + g.Name + " with indices " +
                    //    string.Join(",", g.Indices.ToArray()));
                }
                for (int i = 0; i < allVotes.Length; i++)
                {
                    if (allVotes[i] != MaxNoOfVotes)
                        startFaceOffVoter(PhotonNetwork.PlayerList[i]);
                }
            }
        }
    }

    public void startFaceOffRound(Player p)
    {
        photonView.RPC("RPC_faceOffRound", p);
    }
    public void startFaceOffVoter(Player p)
    {
        photonView.RPC("RPC_faceOffVoter", p);
    }

    public void turnOffTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.gameObject.SetActive(true);
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
        roundConfigurator.setTitleText("Seve letter Round");
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

}
