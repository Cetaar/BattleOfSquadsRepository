using UnityEngine;
public class Pause : MonoBehaviour
{
    private bool _isPause = false;
    [Header("�� 0 �� 1")]
    [SerializeField] private float _indexPause;
    public void PutPause()
    {
        _isPause = !_isPause;
        if (!_isPause)
        {
            GlobalTime.TimeSpeed = _indexPause;
        }
        else
        {
            GlobalTime.TimeSpeed = 1f;
        }
    }
}
