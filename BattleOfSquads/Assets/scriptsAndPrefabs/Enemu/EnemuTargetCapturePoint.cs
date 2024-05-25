using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemuTargetCapturePoint : MonoBehaviour
{
    [SerializeField] private TargetForEnemu _targetForEnemu;
    [SerializeField] private CapturingPoint _capturingPoint;

    private void OnEnable()
    {
        _capturingPoint.ChangingCapturedByWhom += CheckingForNecessaryChange;
    }
    private void OnDisable()
    {
        _capturingPoint.ChangingCapturedByWhom -= CheckingForNecessaryChange;
    }
    private void CheckingForNecessaryChange()
    {
        if(_capturingPoint.CapturedByWhom == EnemuGroupManager.instance.NumberCommand)
        {
            _targetForEnemu.IsCompleted = true;
        }
        else
        {
            _targetForEnemu.IsCompleted = false;
        }
    }
} 
