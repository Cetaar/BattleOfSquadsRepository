using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private float _hp;
    private float _hpMax;
    private DescriptionUnit _unit;
    private Death _unitDeath;
    [SerializeField] private ForSlider _hpBar;

    [SerializeField] private float TimerForTemporaryHP;
    private void Start()
    {
        _unit = GetComponent<DescriptionUnit>();

        _unitDeath = GetComponent<Death>();

        _hpMax = _unit.HP;
        _hp = _unit.HP;
        _hpBar.ChangingValues(_hpMax, _hp);
    }
    public bool ChangingHp(float n)
    {
        _hpBar.TemporaryActivation(TimerForTemporaryHP);
        _hp += n;
        if(_hp > _hpMax)
        {
            _hp = _hpMax;
            _hpBar.ShiftSlider(n);
            return false;
        }
        else if(_hp < 0)
        {
            _hp = 0;
            _hpBar.ShiftSlider(n);
            _unitDeath.Death();
            return false;
        }
        _hpBar.ShiftSlider(n);
        return true;
    }    
}
