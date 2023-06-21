using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.Demo.PunBasics;

public class Room : MonoBehaviour, IOnEventCallback
{
    [SerializeField] TMP_Text Txt_RoomName;
    [SerializeField] TMP_Text Txt_PlayersCount;
    [SerializeField] TMP_Text Txt_TimeLeftText;

    public float _starttime;
    public float _endtime;
    [SerializeField] bool _startTimer;
    //[SerializeField] Text _timertext;
    [SerializeField] Slider _timeSlider;
    [SerializeField] float _currenttime;
    //[SerializeField] GameManager gameManager;
    //[SerializeField] bool autoStart;
    [SerializeField] UnityEvent OnTimerEnd;
    //[SerializeField] bool votingTimer;
    public bool StartTime
    {
        get
        {
            return _startTimer;
        }
        set
        {
            _startTimer = value;
        }
    }



    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnEventReceived event
        //PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    bool timeReceived;
    public void OnEvent(EventData photonEvent)
    {
        // Check if the event is the timer start event
        if (photonEvent.Code == 2)
        {
            if (timeReceived == false)
            {
                //Debug.Log("Timer Event Received");
                // Extract the timer start time from the event data
                object[] eventData = (object[])photonEvent.CustomData;
                if (eventData.Length > 0)
                {
                    _currenttime = Convert.ToSingle(eventData[0]);
                }
                //_currenttime = (float)temp;
                StartTime = true;
                timeReceived = true;
            }
        }
    }

    private void Update()
    {
        if (StartTime)
        {
            //if (_currenttime > _endtime)
            //int currentTime = (int)(PhotonNetwork.ServerTimestamp - _starttime);
            if (_currenttime > _endtime)
            {
                _currenttime -= Time.deltaTime;
                //_timertext.text = Mathf.FloorToInt(_currenttime%60).ToString();
                Txt_TimeLeftText.text = "Time Left: " + Mathf.FloorToInt(_currenttime % 60).ToString();
                //_timeSlider.value = _currenttime;
                //_timeSlider.value = _currenttime;
                //_currenttime -= Time.deltaTime;

                if (PhotonNetwork.IsMasterClient)
                {
                    object[] eventData = new object[] { _currenttime };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(2, eventData, raiseEventOptions, SendOptions.SendReliable);
                }

            }
            else
            {

                StartTime = false;
                OnTimerEnd.Invoke();
                //gameManager.OnAnswerTimeComplete();
            }
        }
    }

    public void StartTimer()
    {
        UnityEngine.Debug.Log("Starting Timer");
        // Broadcast the timer start time to all clients
        object[] eventData = new object[] { _currenttime };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(2, eventData, raiseEventOptions, SendOptions.SendReliable);
        Txt_TimeLeftText.gameObject.SetActive(true);
        StartTime = true;
        _currenttime = _starttime;
    }

    public void setRoomStats(string roomName,int PlayerCount)
    {
        Txt_RoomName.text = roomName;
        Txt_PlayersCount.text = PlayerCount.ToString() + "/" + 4 + " Players Joined.";
    }
}
