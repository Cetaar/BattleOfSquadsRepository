using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetForEnemu : MonoBehaviour
{
    [SerializeField] private bool _isCompleted = false;
    private EnemuGroupManager _enemuGroupManager;

    private TargetPoint _targetPoint;

    [SerializeField] private GameObject _visibleForCreate;
    private void Start()
    {
        _enemuGroupManager = EnemuGroupManager.instance;
        _visibleForCreate.SetActive(false);
    }
    public bool IsCompleted
    {
        get
        {
            return _isCompleted;
        }
        set
        {
            _isCompleted = value;
            _enemuGroupManager.ChangingTargetActivation();
        }
    }
}
