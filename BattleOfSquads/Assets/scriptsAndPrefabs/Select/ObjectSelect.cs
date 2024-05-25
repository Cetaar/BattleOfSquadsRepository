using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectSelect : MonoBehaviour
{


    public List<GameObject> noBug = new();


    [SerializeField] private string[] _playerUnitTags;
    [SerializeField] private string[] _surfaces;
    [SerializeField] private BuildManager BuildManager;
    [SerializeField] private UnitManager UnitManager;
    [SerializeField] private Texture2D SelectTexture;
    [SerializeField] private GameObject TargetPoint;
    public Vector3? firstPoint = null;
    public static ObjectSelect instance;

    private int _player—ommand;

    private ISelect _selectedOne;

    private void Awake()
    {
        instance = this;
    }

    public List<ISelect> selectObjects = new List<ISelect>();

    private void Start()
    {
        _player—ommand = DescriptionCommand.PlayerCommand;
    }
    private void Update()
    {
        noBug = selectObjects.OfType<GameObject>().ToList();

    }

    public void SetFirstPoint(RaycastHit hit, Vector3 FirstPoint)
    {
        _selectedOne = null;
        if (hit.transform != null)
        {
            {
                firstPoint = FirstPoint;
            }
        }
        if (CheckingThatOur(hit.transform))
        {
            _selectedOne = hit.transform.GetComponent<ISelect>();
        }
    }
    public void SetSecondPoint()
    {
        if (firstPoint != null)
        {
            Check((Vector3)firstPoint, Input.mousePosition);
            if (_selectedOne != null)
            {
                if (!selectObjects.Contains(_selectedOne))
                {
                    selectObjects.Add(_selectedOne);
                    _selectedOne.OnSelect(selectObjects.Count - 1);
                    MainCanvas.UserMenu.GetComponent<ForUserMenu>().SetAllSelectedObjectsIU();
                }
            }
            firstPoint = null;
        }
    }
    public void ClearsSelectObjects()
    {
        for (int i = 0; i < selectObjects.Count; i++)
        {
            selectObjects[i].OnDeselect();
        }
        selectObjects.Clear();
        MainCanvas.UserMenu.GetComponent<ForUserMenu>().SetAllSelectedObjectsIU();
    }
    public bool SetTarget(List<ISelect> SelectObjects, RaycastHit hit)
    {
        if (SelectObjects.Count > 0)
        {
            if (hit.transform != null && CheckingThatsurfaces(hit.transform))
            {


                GameObject target = Instantiate(TargetPoint, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                TargetPoint targetPoint = target.GetComponent<TargetPoint>();
                for (int i = 0; i < SelectObjects.Count; i++)
                {
                    SelectObjects[i].OnSetTarget(targetPoint);
                }
                return true;
            }
            return false;
        }
        return false;
    }

    private void OnGUI()
    {
        if (firstPoint != null)
        {
            GUI.DrawTexture(GetRectFromPoints((Vector3)firstPoint, Input.mousePosition), SelectTexture);
        }
    }
    private Rect GetRectFromPoints(Vector3 one, Vector3 two)
    {
        float height = two.x - one.x;
        float Width = (Screen.height - two.y) - (Screen.height - one.y);
        return new Rect(one.x, Screen.height - one.y, height, Width);
    }

    private void Check(Vector3 firstPoint, Vector3 SecondPoint)
    {
        if(SecondPoint.x < firstPoint.x)
        {
            var x1 = firstPoint.x;
            firstPoint.x = SecondPoint.x;
            SecondPoint.x = x1;
        }
        Rect rect = GetRectFromPoints(firstPoint, SecondPoint);
        foreach (Unit unit in UnitManager.instance.GetAllUnits(_player—ommand))
        {
            Vector3[] points = { unit.GetComponent<Collider>().bounds.max, unit.GetComponent<Collider>().bounds.min, unit.transform.position };
            foreach(Vector3 point in points)
            {
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(point);
                screenPoint = new Vector3(screenPoint.x, Screen.height - screenPoint.y, screenPoint.z);
                if (rect.Contains(screenPoint))
                {
                    selectObjects.Add(unit);
                    unit.OnSelect(selectObjects.Count - 1);
                    MainCanvas.UserMenu.GetComponent<ForUserMenu>().SetAllSelectedObjectsIU();
                    break;
                }
            }
        }
    }
    private bool CheckingThatOur(Transform Object)
    {
        if (Object != null)
        {
            string _tag = Object.tag;
            for (int i = 0; i < _playerUnitTags.Length; i++)
            {
                if (_tag == _playerUnitTags[i])
                {
                    Debug.Log($"LayerMask.LayerToName(Object.gameObject.layer = {LayerMask.LayerToName(Object.gameObject.layer)}");
                    if (LayerMask.LayerToName(Object.gameObject.layer) == _player—ommand.ToString())
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
    private bool CheckingThatsurfaces(Transform Object)
    {
        string _tag = Object.tag;
        for (int i = 0; i < _surfaces.Length; i++)
        {
            if (_tag == _surfaces[i])
            {
                return true;
            }
        }
        return false;
    }
}