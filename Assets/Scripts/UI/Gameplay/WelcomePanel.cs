using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomePanel : MonoBehaviour
{
    [SerializeField] Text TimerTxt;
    [SerializeField] float CurrentTime, EndTime;
    [SerializeField] bool StartTimer;
    [SerializeField] UiController UiController;
    private void OnEnable()
    {
       Invoke("StartGame",1f);
    }

    private void StartGame()
    {
        //TimerTxt.gameObject.SetActive(true);
        CurrentTime = EndTime;
        StartTimer = true;
    }

    private void Update()
    {
        if (StartTimer)
        {
            if (CurrentTime >= 0)
            {
                CurrentTime -= Time.deltaTime;
                TimerTxt.text = "Starting Three Letter Round in: " + Mathf.FloorToInt(CurrentTime % 60).ToString();
            }
            else
            {
                UiController.Start3LetterRound();
                StartTimer = false;
                this.gameObject.SetActive(false);
            }
        }
    }
}
