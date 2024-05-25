using UnityEngine;
using System;
public class GameStat
{
    public static event Action ChangingActionState;

    private static ActionState _actionState = ActionState.Free;
    public static ActionState actionState 
    {
        get 
        {
            return _actionState;
        }
        set
        {
            Debug.Log($"actionState = {value}");
            _actionState = value;
            ChangingActionState?.Invoke();
        }
    }
}
public enum ActionState { Free, Build, UsingPossibleActions}
