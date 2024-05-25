using UnityEngine;
using System;

public class FullPause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseIndicator;
    private bool _isNoPause = true;
    public void PutFullPause()
    {
        _isNoPause = !_isNoPause;
        if (_isNoPause)
        {
            GlobalTime.TimeSpeed = 1f;
        }
        else
        {
            GlobalTime.TimeSpeed = 0f;
        }
        _pauseIndicator.SetActive(!_isNoPause);
    }
}
