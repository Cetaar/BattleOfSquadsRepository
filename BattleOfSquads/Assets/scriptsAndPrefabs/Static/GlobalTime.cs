using UnityEngine;
public static class GlobalTime
{
    private static float _timeSpeed = 1;
    public static float TimeSpeed
    {
        get
        {
            return _timeSpeed;
        }
        set
        {
            if (_timeSpeed != value)
            {
                _timeSpeed = value;
                Time.timeScale = value;
            }
        }
    }
}
