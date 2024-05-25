using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyInput : MonoBehaviour
{ 
    
    public static MyInput instance;

    [SerializeField] private FullPause _timeManager;
    
    private ObjectSelect _objectSelect;
    private CameraMoveDefault CameraMoveDefault;

    public bool DontResetSelection = true;

    private Vector3 _firstPointForObjectSelect;

    private bool _buttonDown = false;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CameraMoveDefault = CameraMoveDefault.instance;
        _objectSelect = ObjectSelect.instance;
    }

    private void Update()
    {
        CheckingForMouseClicks();
        ClickingOnButtons();
    }
    private void ClickingOnButtons()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            CameraMoveDefault.TranslateCamera2();
        }
        else
        {
            CameraMoveDefault.TranslateCamera();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MainCanvas.PauseMenu.activeSelf)
            {
                _timeManager.PutFullPause();
            }
            else
            {
                MainCanvas.ButtonMenu.GetComponent<Button>().onClick.Invoke();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            CameraMoveDefault.SpeedBoost();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            DontResetSelection = true;
        }
        else
        {
            DontResetSelection = false;
        }
    }
    private void CheckingForMouseClicks()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            CameraMoveDefault.ZomeCamera();
        }
        if (Input.GetMouseButtonDown(0))
        {
            _buttonDown = EventSystem.current.IsPointerOverGameObject();
            _firstPointForObjectSelect = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            { 
                if (GameStat.actionState == ActionState.Free)
                {
                    if (_objectSelect.firstPoint == null) _objectSelect.SetFirstPoint(CastFromCursor(), _firstPointForObjectSelect);
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (GameStat.actionState == ActionState.Free)
            {
                if (!DontResetSelection)
                {
                    if (!_buttonDown)
                    {
                        _objectSelect.ClearsSelectObjects();
                    }
                }
                _objectSelect.SetSecondPoint();
            }
        }


        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                CameraMoveDefault.RotationCamera();
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (GameStat.actionState == ActionState.Free)
            {
                if (!Input.GetKey(KeyCode.LeftControl))
                {
                    RaycastHit hit = CastFromCursor();
                    _objectSelect.SetTarget(_objectSelect.selectObjects, hit);
                }
            }
        }
    }
    public RaycastHit CastFromCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        LayerMask layerMask = LayerMask.GetMask("Trigger");
        Physics.Raycast(ray, out hit, Mathf.Infinity, ~layerMask);
        return hit;
    }
}
