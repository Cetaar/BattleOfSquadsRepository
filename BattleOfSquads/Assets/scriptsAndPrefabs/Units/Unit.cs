using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour, ISelect, Death{
    public int teamNumber = 0;
    [SerializeField] private TargetPoint _target = null;
    private NavMeshAgent _agent;
    public bool selected = false;

    [SerializeField] private ForSlider _hpBar;

    public bool SpecialSituation = false;

    private CapturingPoint _capturingPoint;
    public void SerCapturingPoint(CapturingPoint Point)
    {
        _capturingPoint = Point;
    }
    public NavMeshAgent Agent
    {
        get
        {
            if (_agent == null)
            {
                _agent = GetComponent<NavMeshAgent>();
            }
            return _agent;
        }
        set
        {
            _agent = value;
        }
    }
    private Vector3 targetPosition;
    private void Start()
    {
        UnitManager.instance.AddUnits(this);
        if(_hpBar != null)
        {
            if(GetComponent<DescriptionUnit>()!= null)
            {
                _hpBar.ChangingValues(GetComponent<DescriptionUnit>().HP, GetComponent<DescriptionUnit>().HP);
            }
            else Debug.Log("-2-");
        }
        else Debug.Log("-1-");

    }
    private void SetTargetForMoving(Vector3 TargetPosition)
    {
        if (!Agent.SetDestination(TargetPosition))
        {
            MainCanvas.Warning.GetComponent<ForWarning>().WarningOutput("Отмена, неправильное назначение!");
            _target.RemoveLink(gameObject);
            _target = null;
            Agent.SetDestination(transform.position);
        }
    }
    void Update()
    {
        if (!SpecialSituation)
        {
            if (_target != null)
            {
                SetTargetForMoving(targetPosition);
            }
        }
        else
        {
            if(_target!= null)
            {
                _target.RemoveLink(gameObject);
            }
        }
    }

    public void OnDeselect()
    {
        _hpBar.IsActivation = false;
        selected = false;
        if (_target != null)
        {
            _target.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(false);
    }
    public void OnSelect(int num)
    {
        _hpBar.IsActivation = true;
        selected = true;
        if (_target != null)
        {
            _target.gameObject.SetActive(true);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }


    public void OnSetTarget(TargetPoint targetPoint)
    {
        SpecialSituation = false;
        if (_target != null)
        {
            _target.RemoveLink(gameObject);
        }
        _target = targetPoint;
        _target.AddLink(gameObject);
        targetPosition = _target.transform.position;
        Agent.stoppingDistance = Random.Range(1.0f, 2.0f);
    }
    public void Death()
    {
        OnDeselect();
        UnitManager.instance.RemoveUnits(this);
        Instantiate(GetComponent<DescriptionUnit>().Skeleton);
        if (_capturingPoint != null)
        {
            _capturingPoint.DestructionCaptureUnit(this);
        }

        Destroy(gameObject);
    }
}
