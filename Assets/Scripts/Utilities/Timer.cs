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

using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public float _starttime;
    public float _endtime;
    [SerializeField] bool _startTimer;
    [SerializeField] Text _timertext;
    [SerializeField] Slider _timeSlider;
    [SerializeField] float _currenttime;
    [SerializeField] GameManager gameManager;
    [SerializeField] bool autoStart;
    [SerializeField] UnityEvent OnTimerEnd;
    [SerializeField] bool votingTimer;
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

    //private void Start()
    //{
    //    // Subscribe to the OnEventReceived event
    //    PhotonNetwork.NetworkingClient.EventReceived += OnEventReceived;
    //}

    private void OnEnable()
    {
        resetTimer  ();
        if (autoStart)
        {
            StartTimer();
        }
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnEventReceived event
        //PhotonNetwork.NetworkingClient.EventReceived -= OnEventReceived;
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void resetTimer()
    {
        _timertext.text = "";
        _currenttime = _starttime;
        //Debug.Log(" " + _currenttime.ToString());
        _timeSlider.value = _starttime;
        _timeSlider.minValue = _endtime;
        _timeSlider.maxValue = _starttime;
    }

    public void StartTimer()
    {  
        // Broadcast the timer start time to all clients
        object[] eventData = new object[] { _currenttime };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(1, eventData, raiseEventOptions, SendOptions.SendReliable);

        _startTimer = true;
        // Only the master client can start the timer
        //_starttime = (int)PhotonNetwork.ServerTimestamp;
        

        //if (PhotonNetwork.IsMasterClient)
        //{
        //}

    }
    int NoOfPlayerSumbittedAnswer;
    private void Update()
    {
        //Debug.Log("StartTime Is: " + PhotonNetwork.ServerTimestamp.ToString());
        //Debug.Log("StartTime Is: " + PhotonNetwork.Time.ToString());
        if (_startTimer)
        {

            if (gameManager.noOfAnswersSubmitted != 4 || votingTimer == true)
            {
                //if (_currenttime > _endtime)
                //int currentTime = (int)(PhotonNetwork.ServerTimestamp - _starttime);
                if (_currenttime > _endtime)
                {
                    _currenttime -= Time.deltaTime;
                    //_timertext.text = Mathf.FloorToInt(_currenttime%60).ToString();
                    _timertext.text = Mathf.FloorToInt(_currenttime % 60).ToString();
                    //_timeSlider.value = _currenttime;
                    _timeSlider.value = _currenttime;
                    //_currenttime -= Time.deltaTime;

                    if (PhotonNetwork.IsMasterClient)
                    {
                        object[] eventData = new object[] { _currenttime };
                        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                        PhotonNetwork.RaiseEvent(1, eventData, raiseEventOptions, SendOptions.SendReliable);
                    }

                }
                else
                {

                    _startTimer = false;
                    OnTimerEnd.Invoke();
                    //gameManager.OnAnswerTimeComplete();
                }
            }
            else
            {
                GameManager.updateAnswersSubmittedNumber(0);
                _startTimer=false;
            }
        }
    }

    bool timeReceived;
    public void OnEvent(EventData photonEvent)
    {
        // Check if the event is the timer start event
        if (photonEvent.Code == 1)
        {
            if (timeReceived == false)
            {
                Debug.Log("Timer Event Received");
                // Extract the timer start time from the event data
                object[] eventData = (object[])photonEvent.CustomData;
                if(eventData.Length > 0)
                {
                    _currenttime = Convert.ToSingle(eventData[0]);
                }
                //_currenttime = (float)temp;
                _startTimer = true;
                timeReceived = true;
            }
        }
        //throw new System.NotImplementedException();
    }
}
