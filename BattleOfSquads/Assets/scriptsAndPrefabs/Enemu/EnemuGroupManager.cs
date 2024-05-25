using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class EnemuGroupManager : MonoBehaviour
{
    private List<EnemuGroup> _groups = new List<EnemuGroup>();
    [SerializeField] private TargetForEnemu[] _targets;

    public int NumberCommand;

    public GameObject TargetPointPrefab;
    
    public static EnemuGroupManager instance;

    public event Action ChangingTarget;

    public void ChangingTargetActivation()
    {
        ChangingTarget?.Invoke();
    }
    void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        foreach (EnemuGroup enemuGroup in _groups)
        {
            enemuGroup.SetListTarget(_targets);
        }
    }
    public TargetForEnemu[] SetListTarget()
    {
        return _targets;
    }
    public void AddGroupToEnemuGroupManager(EnemuGroup enemuGroup)
    {
        _groups.Add(enemuGroup);
    }
}
