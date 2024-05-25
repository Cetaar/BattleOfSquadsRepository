using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForButtonForAllSelectedObject : MonoBehaviour
{
    public DescriptionUnit descriptionUnit;
    public List<ISelect> selectedGroup;
    [SerializeField] private float SpeedWriteDescription;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private ManagerPossibleActions _managerPossibleActions;

    private Coroutine _coroutine;
    private void Start()
    {
        _managerPossibleActions = MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>();
        _tmpText = MainCanvas.SelectedObject.transform.GetChild(0).GetChild(0).GetComponent<TMP_Text>();
    }
    public void Clear()
    {
        if (_tmpText != null)
        {
            _tmpText.text = "";
        }
        else
        {
            //Debug.Log("_tmpText = null");
        }
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _tmpText.text = "";
        }
    }
    public void SelectGroup()
    {
        Clear();
        _coroutine = StartCoroutine(WriteText(SpeedWriteDescription, descriptionUnit.Description, _tmpText));
        _managerPossibleActions.SelectedGroup = selectedGroup;
        _managerPossibleActions.ClearActions();
        _managerPossibleActions.AddActions(descriptionUnit.PossibleActionsPrefabs);
    }

    IEnumerator WriteText(float Speed, string Text, TMP_Text TMPText)
    {
        string currentText = "";
        for (int i = 0; i <= Text.Length; i++)
        {
            currentText = Text.Substring(0, i);
            TMPText.text = currentText;
            yield return new WaitForSeconds(Speed);
        }
    }
}
