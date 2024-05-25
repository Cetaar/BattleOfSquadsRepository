using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDead : MonoBehaviour, Target
{
    [SerializeField] private float _coefficiateDestruction = 0.01f;
    [SerializeField] private float _matter;
    [SerializeField] private List<Engineer> _linkEngineers = new();
    private float _remains;

    [SerializeField] private ForSlider _hpBar;
    private void Start()
    {
        _matter = GetComponent<DescriptionUnit>().Matter - GetComponent<DescriptionUnit>().Matter * _coefficiateDestruction;
        UnitManager.instance.AddUnitsDead(this);

        _hpBar.ChangingValues(GetComponent<DescriptionUnit>().Matter, _matter);
    }
    public float Minus(float nun)
    {
        if (_matter - nun <= 0)
        {
            _remains = _matter;
            _matter = 0;
            _hpBar.ShiftSlider(_remains);
            return _remains;
        }
        _matter -= nun;
        _hpBar.ShiftSlider(_matter);
        return nun;
    }
    public void Dead()
    {
        UnitManager.instance.RemoveUnitsDead(this);
        foreach(Engineer engineer in _linkEngineers)
        {
            engineer.ExtractObjects.Remove(this);
            engineer.NextExtract();
        }
        Destroy(gameObject);
    }
    public void RemoveLink(GameObject Object)
    {
        _linkEngineers.Remove(Object.GetComponent<Engineer>());
        if (_linkEngineers.Count == 0)
        {
            _hpBar.IsActivation = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    public void AddLink(GameObject Object)
    {
        _hpBar.IsActivation = true;
        transform.GetChild(0).gameObject.SetActive(true);
        _linkEngineers.Add(Object.GetComponent<Engineer>());
    }
}
