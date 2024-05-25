using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ForSlider : MonoBehaviour
{
    [SerializeField] private Color _colorForHpBar;
    [SerializeField] private Image _fill;

    [SerializeField] private Slider _slider;

    [SerializeField] private bool _isActivation = false;
    private Vector3 _normalScale;

    private Camera _camera;

    [SerializeField] private float TimerForTemporaryActivation;
    public bool IsActivation
    {
        get
        {
            return _isActivation;
        }
        set
        {
            _isActivation = value;
            Activation();
        }
    }
    private void Start()
    {
        _slider.interactable = false;
        //_fill.color = _colorForHpBar;
        _normalScale = gameObject.transform.localScale;
        Activation();
        _camera = Camera.main;
    }
    private void Update()
    {
        if(TimerForTemporaryActivation > 0)
        {
            TimerForTemporaryActivation -= Time.deltaTime;
        }
        else
        {
            TimerForTemporaryActivation = 0;
        }
    }
    public void TemporaryActivation(float Timer)
    {
        TimerForTemporaryActivation = Timer;
        Activation();
    }
    public void ChangingValues(float maxV, float V)
    {
        _slider.maxValue = maxV;
        _slider.value = V;
    }

    public void ShiftSlider(float n)
    {
        _slider.value = n;       
    }
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        transform.Rotate(0, 180, 0);
    }
    private void Activation()
    {
        if (_isActivation || TimerForTemporaryActivation > 0)
        {
            gameObject.transform.localScale = _normalScale;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, 0, gameObject.transform.localScale.z);
        }
    }
}

