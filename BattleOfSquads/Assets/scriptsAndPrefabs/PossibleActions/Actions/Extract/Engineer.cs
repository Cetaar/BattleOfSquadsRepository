using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Engineer : MonoBehaviour
{
    [SerializeField] private float _distanceExtract;
    [SerializeField] private float _speedExtract;
    [SerializeField] private Transform _fromRay;
    [SerializeField] private Color _colorForLinia;

    private Unit _unit;
    private LineRenderer _lineRenderer;
    [SerializeField] private NavMeshAgent _agent;

    //[SerializeField] private UnitDead _minDistUnit;
    private float _plusToMatter;

    public List<UnitDead> ExtractObjects;

    private UnitDead _unitTarget;
    private UnitDead UnitTarget
    {
        get
        {
            return _unitTarget;
        }
        set
        {
            if (value != _unitTarget) 
            {
                RemoveLinia();
            }
            _unitTarget = value;
        }
    }

    //[SerializeField] private TargetPoint _targetPoint;
    [SerializeField] private float _timer = 1f;
    private void Start()
    {
        _distanceExtract = GetComponent<DescriptionUnit>().MaxDistance;
        _speedExtract = GetComponent<DescriptionUnit>().Damage;

        ExtractObjects = new();
        _agent = GetComponent<NavMeshAgent>();
        _lineRenderer = GetComponent<LineRenderer>();
        _unit = gameObject.GetComponent<Unit>();
    }
    public void ProcessingAndExtraction(List<UnitDead> ExtractObjects)
    {
        _unit.SpecialSituation = true;
        foreach (UnitDead unitDead in ExtractObjects)
        {
            if (!this.ExtractObjects.Contains(unitDead))
            {
                unitDead.AddLink(gameObject);
                this.ExtractObjects.Add(unitDead);
            }
        }
        this.ExtractObjects = DistributionGoals(this.ExtractObjects);
        if (this.ExtractObjects.Count > 0)
        {
            ExtractForOne(this.ExtractObjects[0]);
        }
    }

    List<UnitDead> DistributionGoals(List<UnitDead> ExtractObjects)
    {
        Vector3 callerPosition = transform.position;

        Dictionary<UnitDead, float> distances = new Dictionary<UnitDead, float>();

        foreach (UnitDead obj in ExtractObjects)
        {
            Vector3 objPosition = obj.transform.position;

            float distance = Vector3.Distance(objPosition, callerPosition);

            distances.Add(obj, distance);
        }

        // Сортируем словарь по возрастанию расстояний
        var sortedDistances = distances.OrderBy(pair => pair.Value);

        // Формируем список объектов в порядке увеличения расстояния
        List<UnitDead> sortedList = new List<UnitDead>();
        foreach (var pair in sortedDistances)
        {
            sortedList.Add(pair.Key);
        }

        return sortedList;
    }
    private void Update()
    {
        if (ExtractObjects.Count > 0)
        {
            if (_unit.SpecialSituation == true)
            {
                ExtractForOne(UnitTarget);
            }
            else
            {
                End();
            }
        }
        else
        {
            if (_unit.SpecialSituation == true)
            {
                End();
            }
        }
    }
    private void End()
    {
        RemoveLinia();
        ExtractObjects.ForEach(unitDead => unitDead.RemoveLink(gameObject));
        ExtractObjects.Clear();
        _unit.SpecialSituation = false;
    }
    private void ExtractForOne(UnitDead unit)
    {
        UnitTarget = unit;
        Vector3 unitPosition;
        if (unit != null)
        {
            unitPosition = unit.transform.position;
            //Unit.flagForEngineer = true;
            _agent.SetDestination(unitPosition);

            // Debug.Log(Vector3.Distance(transform.position, unitPosition));
            if (_distanceExtract >= Vector3.Distance(transform.position, unitPosition))
            {
                //Debug.Log(Vector3.Distance(transform.position, unitPosition));

                _agent.SetDestination(transform.position);
                DrawLinia(_fromRay.position, unitPosition);
                if (_timer <= 0)
                {
                    _timer = 1;
                    _plusToMatter = unit.Minus(_speedExtract);
                   // Debug.Log($"_plusToMatter = {_plusToMatter}");
                    if (_plusToMatter != _speedExtract)
                    {
                        unit.Dead();
                        NextExtract();
                    }
                    Materia.PlusMatter(_plusToMatter);
                }
                else
                {
                    _timer -= Time.deltaTime;
                }
            }
        }
    }
    public void NextExtract()
    {
        //Debug.Log("NextExtract");
        if (ExtractObjects.Count > 0)
        {
            UnitTarget = ExtractObjects[0];
        }
    }
    public void RemoveLinia()
    {
        _lineRenderer.positionCount = 0;
        _lineRenderer.positionCount = 2;
    }
    private void DrawLinia(Vector3 From, Vector3 Into)
    {
       // Debug.Log("DrawLinia");
        _lineRenderer.SetPosition(0, From);
        _lineRenderer.SetPosition(1, Into);
        // Устанавливаем цвет луча
        _lineRenderer.startColor = _colorForLinia;
        _lineRenderer.endColor = _colorForLinia;
    }
}
