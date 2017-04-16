using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HighscoreTime
{
    private int _minutes;
    public int Minutes { get { return _minutes; } }

    private int _seconds;
    public int Seconds { get { return _seconds; } }

    private int _milliSeconds;
    public int MilliSeconds { get { return _milliSeconds; } }

    private float _totalTime;
    public float TotalTime { get { return _totalTime; } }

    public void SetTime(float totalTime)
    {
        this._totalTime = totalTime;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        _seconds = ((int)_totalTime);
        _minutes = _seconds / 60;
        _seconds = _seconds % 60;

        _milliSeconds = (int)((_totalTime - _seconds) * 1000.0f);
    }

    public string GetDisplayString()
    {
        string milliSecondsText = _milliSeconds < 10 ? "0" + _milliSeconds.ToString() : _milliSeconds.ToString();
        string secondText = _seconds < 10 ? "0" + _seconds.ToString() : _seconds.ToString();
        string minutesText = _minutes < 10 ? "0" + _minutes.ToString() : _minutes.ToString();

        if (milliSecondsText.Length >= 3)
            milliSecondsText = milliSecondsText.Substring(0, 2);

        return string.Concat(minutesText, ":", secondText, ":", milliSecondsText);
    }
}
