using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndMenu : MonoBehaviour
{
    [SerializeField] Text NameText;
    [SerializeField] Text VotesText;
    [SerializeField] bool starttimer;
    [SerializeField] float timeToLeave = 6;
    [SerializeField] Text TimerText;
    [SerializeField] UiController uiController; 
    public void setEndPanelStats(string name, int votes)
    {
        NameText.text = name;
        VotesText.text = votes.ToString();
    }

    public void onClick_ContinueButton()
    {
        PhotonNetwork.LeaveRoom(true);
        PhotonNetwork.LoadLevel(1);
    }

    public void StartTimer()
    {
        starttimer = true;
        timeToLeave = 6;
    }

    private void Update()
    {
        if (starttimer)
        {
            if (timeToLeave > 0)
            {
                timeToLeave -= Time.deltaTime;
                
                TimerText.text = "("+ Mathf.FloorToInt(timeToLeave % 60).ToString() + ")";
            }
            else
            {
                StartCoroutine(DisconnectAndLoad());
                starttimer = false;
                timeToLeave = 6;
            }
        }

    }

    IEnumerator DisconnectAndLoad()
    {
        //PhotonNetwork.LeaveRoom();
        //while (PhotonNetwork.InRoom)
        //{
        yield return new WaitForSeconds(0);
        //}
        Debug.Log("Disconnect and leaving room");
        //SceneManager.LoadScene(1);
        uiController.loadLobby();
    }
}
