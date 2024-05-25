using System.Collections.Generic;
using UnityEngine;

public class ManagerPossibleActions : MonoBehaviour
{
    public Transform ListPossibleActions;
    [Header("   ")]
    private static AllPossibleActions _currentPossibleActivity;
    
    public static AllPossibleActions CurrentPossibleActivity
    {
        get
        {
            return _currentPossibleActivity;
        }
        set
        {
            Debug.Log($"CurrentPossibleActivity = {value}");
            if (value != AllPossibleActions.Null)
            {
                ManagerPossibleActions _managerPossibleActions = MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>();
                GameStat.actionState = ActionState.UsingPossibleActions;
                _managerPossibleActions.FindAndActivitonAction(_managerPossibleActions.ListPossibleActions, value.ToString());
            }
            else
            {
                GameStat.actionState = ActionState.Free;
            }
            _currentPossibleActivity = value;
        }
    }

    public List<ISelect> SelectedGroup;
    public void ClearActions()
    {
        foreach (Transform child in transform.GetChild(0).transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void AddActions(GameObject[] ButtonPrefabs)
    {
        for (int i = 0; i < ButtonPrefabs.Length; i++)
        {
            GameObject temporaryObject = Instantiate(ButtonPrefabs[i]);
            temporaryObject.transform.SetParent(transform.GetChild(0));
        }
    }
    public void FindAndActivitonAction(Transform parent, string name)
    {
        Transform result = null;
        foreach (Transform child in parent)
        {
            if (child.name == name)
            {
                result = child;
                break;
            }
        }
        if (result != null)
        {
            result.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("result = null");
        }
    }
}
public enum AllPossibleActions {Move, Extract, Null }
