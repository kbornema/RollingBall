using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct HighscoreTime : System.IEquatable<object>
{
    [SerializeField]
    private int _minutes;
    public int Minutes { get { return _minutes; } }

    [SerializeField]
    private int _seconds;
    public int Seconds { get { return _seconds; } }

    [SerializeField]
    private int _milliSeconds;
    public int MilliSeconds { get { return _milliSeconds; } }

    private float _totalTime;
    public float TotalTime { get { return _totalTime; } }

    public void ComputeTotalTime()
    {
        _totalTime = _minutes * 60.0f + _seconds + _milliSeconds / 1000.0f;
    }

    public void SetTime(float totalSeconds)
    {
        this._totalTime = totalSeconds;
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

    public static bool operator <= (HighscoreTime lhs, HighscoreTime rhs)
    {
        return lhs == rhs || lhs < rhs;
    }

    public static bool operator >=(HighscoreTime lhs, HighscoreTime rhs)
    {
        return lhs == rhs || lhs > rhs;
    }

    public static bool operator == (HighscoreTime lhs, HighscoreTime rhs)
    {
        return lhs._minutes == rhs._minutes && lhs._seconds == rhs._seconds && lhs._milliSeconds == rhs._milliSeconds;
    }

    public static bool operator !=(HighscoreTime lhs, HighscoreTime rhs)
    {
        return !(lhs == rhs);
    }

    public static bool operator > (HighscoreTime lhs, HighscoreTime rhs)
    {
        if (lhs._minutes == rhs._minutes)
        {
            if (lhs._seconds == rhs._seconds)
                return lhs._milliSeconds > rhs._milliSeconds;

            else
                return lhs._seconds > rhs._seconds;
        }

        else
            return lhs._minutes > rhs._minutes;
    }

    public static bool operator < (HighscoreTime lhs, HighscoreTime rhs)
    {
        if (lhs._minutes == rhs._minutes)
        {
            if (lhs._seconds == rhs._seconds)
                return lhs._milliSeconds < rhs._milliSeconds;

            else
                return lhs._seconds < rhs._seconds;
        }

        else
            return lhs._minutes < rhs._minutes;
    }



    public override bool Equals(object other)
    {
        if(other is HighscoreTime)
            return this == (HighscoreTime)other;

        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return GetDisplayString();
    }
}
