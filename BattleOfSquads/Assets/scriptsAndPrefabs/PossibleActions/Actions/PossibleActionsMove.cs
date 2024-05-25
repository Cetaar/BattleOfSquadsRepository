using UnityEngine;
using UnityEngine.EventSystems;

public class PossibleActionsMove : MonoBehaviour
{
    private MyInput _myInput;
    private ObjectSelect _objectSelect;
    private RaycastHit hit = new RaycastHit();
    private void Start()
    {
        _myInput = MyInput.instance;
        _objectSelect = ObjectSelect.instance;
    }
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {
                hit = _myInput.CastFromCursor();
            }
            if (Input.GetMouseButtonUp(0))
            {
                _objectSelect.SetTarget(MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>().SelectedGroup, hit);
                ManagerPossibleActions.CurrentPossibleActivity = AllPossibleActions.Null;
                gameObject.SetActive(false);
            }
        }
    }
}
