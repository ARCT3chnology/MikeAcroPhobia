using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Timer : MonoBehaviour
{
    public float _starttime;
    public float _endtime;
    [SerializeField] bool _startTimer;
    [SerializeField] Text _timertext;
    [SerializeField] Slider _timeSlider;
    [HideInInspector] float _currenttime;
    [SerializeField] GameManager gameManager;

    private void OnEnable()
    {
        _currenttime = _starttime;
        //Debug.Log(" " + _currenttime.ToString());
        _timeSlider.value = _starttime;
        _timeSlider.minValue = _endtime;
        _timeSlider.maxValue = _starttime;  
        _startTimer = true;
    }

    private void Update()
    {
        if (_startTimer)
        {
            if (_currenttime > _endtime)
            {
                _currenttime -= Time.deltaTime;
                _timertext.text = Mathf.FloorToInt(_currenttime%60).ToString();
                _timeSlider.value = _currenttime;
            }
            else
            {
                _startTimer = false;
                gameManager.OnAnswerTimeComplete();
            }
        }
    }
}
