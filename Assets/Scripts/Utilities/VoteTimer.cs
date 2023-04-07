using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VoteTimer : MonoBehaviour
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
        _timeSlider.minValue = _endtime;
        _timeSlider.maxValue = _starttime;
    }

    public void StartTimer()
    {
        _startTimer = true;
    }

    int NoOfPlayerSumbittedAnswer;
    private void Update()
    {
        if (_startTimer)
        {
            if (_currenttime > _endtime)
            {
                _currenttime -= Time.deltaTime;
                _timertext.text = Mathf.FloorToInt(_currenttime % 60).ToString();
                _timeSlider.value = _currenttime;
            }
            else
            {
                _startTimer = false;
                
                OnTimerEnd.Invoke();
                //gameManager.OnAnswerTimeComplete();
            }
            
        }
    }
}
