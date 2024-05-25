using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemuGroup : MonoBehaviour
{
   // [SerializeField] private GameObject _groupPrefab;
    [SerializeField] private TargetForEnemu[] _targets;

    [SerializeField] private TargetForEnemu _mainTarget;
    private EnemuGroupManager _enemuGroupManager;

    public TargetPoint targetPoint;
    private void OnEnable()
    {
        _enemuGroupManager = EnemuGroupManager.instance;
        _enemuGroupManager.AddGroupToEnemuGroupManager(this);

        _enemuGroupManager.ChangingTarget += UpdateTarget;
    }
    private void OnDisable()
    {
        _enemuGroupManager.ChangingTarget -= UpdateTarget;
    }
    public void SetListTarget(TargetForEnemu[] targets)
    {
        _targets = targets;
    }
    private void Start()
    {
        _targets = _enemuGroupManager.SetListTarget();
        UpdateTarget();
    }
    private void UpdateTarget()
    {
        foreach(TargetForEnemu target in _targets)
        {
            if (!target.IsCompleted)
            {
                _mainTarget = target;
                break;
            }
        }
        if (_mainTarget != null && !_mainTarget.IsCompleted)
        {
            targetPoint = Instantiate(_enemuGroupManager.TargetPointPrefab, _mainTarget.gameObject.transform.position, Quaternion.identity).GetComponent<TargetPoint>();
            SetTargetForGroup(targetPoint);
        }
    }
    private void SetTargetForGroup(TargetPoint targetPoint)
    {
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject.CompareTag("Unit"))
            {
                child.GetComponent<Unit>().OnSetTarget(targetPoint);
            }
        }
    }
}
