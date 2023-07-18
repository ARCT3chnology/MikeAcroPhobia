using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VoteTimer : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [Tooltip("Set time here for all clients and for all rounds")]
    public float _starttime;
    public float _endtime;
    [SerializeField] bool _startTimer;
    [SerializeField] Text _timertext;
    [SerializeField] Slider _timeSlider;
    [HideInInspector] float _currenttime;
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

    private void OnEnable()
    {
        if (autoStart)
        {
            StartTimer();
        }
        _timertext.text = "";
        _currenttime = _starttime;
        //Debug.Log(" " + _currenttime.ToString());
        _timeSlider.value = _starttime;
        _timeSlider.value = _endtime;
        _timeSlider.minValue = _endtime;
        _timeSlider.maxValue = _starttime;
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public void StartTimer()
    {
        _startTimer = true;
        // Broadcast the timer start time to all clients
        object[] eventData = new object[] { _currenttime };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(2, eventData, raiseEventOptions, SendOptions.SendReliable);
    }

    int NoOfPlayerSumbittedAnswer;
    private void Update()
    {
        if (_startTimer)
        {
            if (_currenttime > _endtime)
            {
                _timertext.text = Mathf.FloorToInt(_currenttime % 60).ToString();
                _currenttime -= Time.deltaTime;
                _timeSlider.value = _currenttime;
                
                if (PhotonNetwork.IsMasterClient)
                {
                    object[] eventData = new object[] { _currenttime };
                    RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
                    PhotonNetwork.RaiseEvent(2, eventData, raiseEventOptions, SendOptions.SendReliable);
                }
            }
            else
            {
                _startTimer = false;

                OnTimerEnd.Invoke();
                //gameManager.OnAnswerTimeComplete();
            }
        }
    }

    bool timeReceived;
    public void OnEvent(EventData photonEvent)
    {
        // Check if the event is the timer start event
        if (photonEvent.Code == 2)
        {
            if (timeReceived == false)
            {
                Debug.Log("Timer Event Received");
                // Extract the timer start time from the event data
                object[] eventData = (object[])photonEvent.CustomData;
                if (eventData.Length > 0)
                {
                    _currenttime = Convert.ToSingle(eventData[0]);
                }
                //_currenttime = (float)temp;
                _startTimer = true;
                timeReceived = true;
            }
        }
    }
}
