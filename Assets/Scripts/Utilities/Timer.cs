using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float _starttime;
    [SerializeField] float _endtime;
    [SerializeField] bool _startTimer;
    [SerializeField] Text _timertext;
    [SerializeField] Slider _timeSlider;
    [HideInInspector] float _currenttime;
    
    public Text TimerText { get { return _timertext; } }
    public Slider TimeSlider { get { return _timeSlider; } }
    public float StartTime { get { return _starttime;} }
    public float EndTime { get { return _endtime;} }
    public float CurrentTime { get { return _currenttime;} }
    public bool StartTimer { get { return _startTimer;} }


    private void Start()
    {
        _currenttime = StartTime;
        TimeSlider.minValue = EndTime;
        TimeSlider.maxValue = StartTime;
    }

    private void Update()
    {
        if (StartTimer)
        {
            if (CurrentTime > EndTime)
            {
                _currenttime -= Time.deltaTime;
                TimerText.text = Mathf.FloorToInt(_currenttime%60).ToString();
                TimeSlider.value = _currenttime;
            }
            else
            {
                _startTimer = false;
            }
        }
    }
}
