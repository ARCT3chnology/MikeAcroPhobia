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
using Debug = UnityEngine.Debug;
using UnityEngine.UIElements;
using DG.Tweening.Core.Easing;

public class Room : MonoBehaviourPunCallbacks, IOnEventCallback
{
    [SerializeField] TMP_Text Txt_RoomName;
    [SerializeField] TMP_Text Txt_PlayersCount;
    [SerializeField] TMP_Text Txt_TimeLeftText;

    [SerializeField] UnityEvent OnTimerEnd;
    [SerializeField] bool autoStart;

    public float _starttime;
    public float _currenttime;
    public float _endtime;

    [SerializeField] bool _startTimer;
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
        resetTimer();
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
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

    private void Update()
    {
        if (_startTimer)
        {

            if (_currenttime > _endtime)
            {
                Txt_TimeLeftText.text = "Time Left: " + Mathf.FloorToInt(_currenttime % 60).ToString();
                _currenttime -= Time.deltaTime;
                //_timertext.text = Mathf.FloorToInt(_currenttime%60).ToString();

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

                if (PhotonNetwork.IsMasterClient)
                {
                    OnTimerEnd.Invoke();

                }
            }

        }
    }

    public void resetTimer()
    {
        Txt_TimeLeftText.text = "";
        _currenttime = _starttime;
        //Debug.Log(" " + _currenttime.ToString());
        //_timeSlider.value = _starttime;
        //_timeSlider.minValue = _endtime;
        //_timeSlider.maxValue = _starttime;
    }

    public void StartTimer()
    {
        // Broadcast the timer start time to all clients
        object[] eventData = new object[] { _currenttime };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        PhotonNetwork.RaiseEvent(2, eventData, raiseEventOptions, SendOptions.SendReliable);

        _startTimer = true;
        Txt_TimeLeftText.gameObject.SetActive(true);
    }

    public void PauseTimer()
    {
        _startTimer = false;
    }


    bool timeReceived;

    public void OnEvent(EventData photonEvent)
    {
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

    public void setRoomStats(string roomName,int PlayerCount)
    {
        Txt_RoomName.text = roomName;
        Txt_PlayersCount.text = PlayerCount.ToString() + "/" + MasterManager.Instance._gameSettings.maxPlayerForRoom.ToString() + " Players Joined.";
    }

}
