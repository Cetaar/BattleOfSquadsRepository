using System.Collections.Generic;
using UnityEngine;

public class PossibleActionsExtract: MonoBehaviour
{
    private ManagerPossibleActions _managerPossibleActions;
    [SerializeField] private List<Engineer> _selectedEngineers;


    [SerializeField] private Texture2D _extractTexture;
    private Vector3? _firstPoint;
    [SerializeField] private UnitDead _selectedOne;

    [SerializeField] private List<UnitDead> _extractObjects;
    public List<UnitDead> ExtractObjects
    {
        get
        {
            return _extractObjects;
        }
        set
        {
            _extractObjects = value;
        }
    }
    private void Start()
    {
        _managerPossibleActions = MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>();
    }
    private void OnEnable()
    {
        _managerPossibleActions = MainCanvas.PossibleActions.GetComponent<ManagerPossibleActions>();
        _firstPoint = null;
        ExtractObjects = new();
        _selectedEngineers = new();
        
        foreach (ISelect obj in _managerPossibleActions.SelectedGroup)
        {
            GameObject objectGameObject = (obj as MonoBehaviour).gameObject;
            if (objectGameObject.GetComponent<Engineer>() != null)
            {
                _selectedEngineers.Add(objectGameObject.GetComponent<Engineer>());
            }
        }
    }

    void Update()
    {
        _selectedOne = null;
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = CastFromCursor();
            if (hit.transform != null)
            {
                Debug.Log(hit.transform.tag);
                if (hit.transform.CompareTag("UnitDead"))
                {
                    _selectedOne = hit.transform.GetComponent<UnitDead>();
                }
                _firstPoint = Input.mousePosition;
            }
        }

        if (_firstPoint != null && Input.GetMouseButtonUp(0))
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach (Engineer engineer in _selectedEngineers)
                {
                    foreach (UnitDead unit in engineer.ExtractObjects)
                    {
                        unit.RemoveLink(engineer.gameObject);
                    }
                    engineer.ExtractObjects.Clear();
                }
            }

            Check((Vector3)_firstPoint, Input.mousePosition);

            if (_selectedOne != null)
            {
                if (!ExtractObjects.Contains(_selectedOne))
                {
                    ExtractObjects.Add(_selectedOne);
                }
            }
            
            if (_selectedEngineers.Count > 0)
            {
                _selectedEngineers.ForEach(engineer => engineer.ProcessingAndExtraction(ExtractObjects));
            }
            End();
        }
    }
    private void End()
    {
        _firstPoint = null;
        ManagerPossibleActions.CurrentPossibleActivity = AllPossibleActions.Null;
        gameObject.SetActive(false);
    }
    private void Check(Vector3 firstPoint, Vector3 endPoint)
    {
        if (endPoint.x < firstPoint.x)
        {
            var x1 = firstPoint.x;
            var x2 = endPoint.x;
            firstPoint.x = x2;
            endPoint.x = x1;
        }

        Rect rect = GetRectFromPoints(firstPoint, endPoint);
        foreach (UnitDead unit in UnitManager.instance.GetAllUnitsDead())
        {
            Vector3[] points = { unit.GetComponent<Collider>().bounds.max, unit.GetComponent<Collider>().bounds.min, unit.transform.position };
            foreach (Vector3 point in points)
            {
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(point);
                screenPoint = new Vector3(screenPoint.x, Screen.height - screenPoint.y, screenPoint.z);
                if (rect.Contains(screenPoint))
                {
                    ExtractObjects.Add(unit);
                    break;
                }
            }
        }
    }
    private RaycastHit CastFromCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit;
    }
    private Rect GetRectFromPoints(Vector3 one, Vector3 two)
    {
        float height = two.x - one.x;
        float Width = (Screen.height - two.y) - (Screen.height - one.y);
        return new Rect(one.x, Screen.height - one.y, height, Width);
    }
    private void OnGUI()
    {
        if (_firstPoint != null && _extractTexture != null)
        {
            GUI.DrawTexture(GetRectFromPoints((Vector3)_firstPoint, Input.mousePosition), _extractTexture);
        }
    }
}
