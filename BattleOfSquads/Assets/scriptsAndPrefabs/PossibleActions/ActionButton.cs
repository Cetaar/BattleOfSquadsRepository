using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private AllPossibleActions _nameActions;
    public void Activation()
    {
        ManagerPossibleActions.CurrentPossibleActivity = _nameActions;
    }
}
