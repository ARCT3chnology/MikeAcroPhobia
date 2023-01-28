using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    [SerializeField] GameObject _ThreeLetterRoundPanel;
    [SerializeField] GameObject _WelcomePanel;
    [SerializeField] GameObject _VotingPanel;

    [HideInInspector] public GameObject welcomePanel { get { return _WelcomePanel; } }
    [HideInInspector] public GameObject threeLetterRound { get { return _ThreeLetterRoundPanel; } }
    [HideInInspector] public GameObject votingPanel { get { return _VotingPanel; } }

    private void Start()
    {
        welcomePanel.SetActive(true);
    }

    public void Start3LetterRound()
    {
        threeLetterRound.SetActive(true);
    }

    public void turnOffTextPanel()
    {
        threeLetterRound.transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        threeLetterRound.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = "Voting Round";
        votingPanel.SetActive(true);
    }
}
