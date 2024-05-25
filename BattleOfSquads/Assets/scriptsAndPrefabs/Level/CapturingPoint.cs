using System.Collections.Generic;
using UnityEngine;
using System;

public class CapturingPoint : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> ObjectsForChangeColor;

    //
    [SerializeField] private List<Renderer> _rendsSpecial;
    [SerializeField] private Material[] _MaterialsSpecial = new Material[2];
    //

    [SerializeField] private List<Renderer> _rends;
    private List<Material> _teamMaterials;
    [SerializeField] private GameObject _objectChange;


    [SerializeField] private int _capturedByWhom;

    [SerializeField] private Material _defaultMaterial;
    private int _sum = 0;
    private void Awake()
    {
        ChangeMaterial(_defaultMaterial);
        ChangeMaterialSpecial(_MaterialsSpecial[1]);

    }
    private void ChangeColor(Color ColorTo)
    {
        for (int i = 0; i < ObjectsForChangeColor.Count; i++)
        {
            ObjectsForChangeColor[i].color = ColorTo;
        }
    }
    private void ChangeMaterialSpecial(Material material)
    {
        foreach (Renderer renderer in _rendsSpecial)
        {
            renderer.material = material;
        }
    }
    private void ChangeMaterial(Material material)
    {
        foreach (Renderer renderer in _rends)
        {
            renderer.material = material;
        }
    }

    public event Action ChangingCapturedByWhom;

    public int CapturedByWhom
    {
        get
        {
            return _capturedByWhom;
        }
        set
        {
            _capturedByWhom = value;
            ChangingCapturedByWhom?.Invoke();
            foreach (Command command in DescriptionCommand.Commands)
            {
                if (command.Number == _capturedByWhom)
                {
                    ChangeMaterial(_teamMaterials[_layerCapturing]);
                    _objectChange.layer = command.LayerNumber;
                    ChangeColor(command.ColorOfCommand);

                    if (command.Number == 1)
                    {
                        ChangeMaterialSpecial(_MaterialsSpecial[0]);
                    }
                    else
                    {
                        ChangeMaterialSpecial(_MaterialsSpecial[1]);
                    }
                }
            }
        }
    }

    private int _layer;
    [SerializeField] private int[] _numberUnitsDifferentTeams;

    private int _maxCaptureIndex;
    private bool _isCapture = false;

    [SerializeField] private ForSlider _slider;
    public bool IsCapture 
    {
        get
        {
            return _isCapture;
        }
        set
        {
            _isCapture = value;
        }
    }
    [SerializeField] private float _maxCaptureProcess = 100;
    [SerializeField] private float _captureProcess;
    public float CaptureProcess
    {
        get
        {
            return _captureProcess;
        }
        set
        {
            _captureProcess = value;
            if (_captureProcess > 0)
            {
                _slider.IsActivation = true;
            }
            else
            {
                _slider.IsActivation = false;
            }
        }
    }
    [SerializeField] private float _captureSpeed;

    private int _lastLayerCapturing = -1;
    private int _layerCapturing = -1;
    private void Start()
    {
        _numberUnitsDifferentTeams = new int[DescriptionCommand.Commands.Count];
           
        _captureProcess = 0;
        _teamMaterials = new();
        foreach (Command command in DescriptionCommand.Commands)
        {
            _teamMaterials.Add(command.MaterialOfPoint);
        }
        _slider.ChangingValues(_maxCaptureProcess, 0);
    }
    private void Update()
    {
        if (IsCapture)
        {
            CaptureProcess += Time.deltaTime * _captureSpeed;
            _slider.ShiftSlider(CaptureProcess);
            if (CaptureProcess >= _maxCaptureProcess)
            {
                EndCapture();
                CapturedByWhom = _layerCapturing + 1;
            }
        }
        if(_sum == 0)
        {
            ResetCaptureProcess();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            other.GetComponent<Unit>().SerCapturingPoint(this);
            _sum++;
            _layer = int.Parse(LayerMask.LayerToName(other.gameObject.layer));
            _numberUnitsDifferentTeams[_layer - 1] += 1;
            if (_layer != CapturedByWhom)
            {
                Capturing();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Unit"))
        {
            _sum--;
            _layer = int.Parse(LayerMask.LayerToName(other.gameObject.layer));
            _numberUnitsDifferentTeams[_layer - 1] -= 1;
            Capturing();
        }
    }
    public void DestructionCaptureUnit(Unit unit)
    {
        _sum--;
        _layer = int.Parse(LayerMask.LayerToName(unit.gameObject.layer));
        _numberUnitsDifferentTeams[_layer - 1] -= 1;
        Capturing();
    }
    public void Capturing()
    {
        _maxCaptureIndex = 0;
        for(int i = 0; i < _numberUnitsDifferentTeams.Length; i++)
        {
            if(_numberUnitsDifferentTeams[i] > _maxCaptureIndex)
            {
                _maxCaptureIndex = _numberUnitsDifferentTeams[i];
                _layerCapturing = i;
            }
        }
        if(_lastLayerCapturing != _layerCapturing)
        {
            IsCapture = true;
            ChangingTeam();
        }
    }
    private void ChangingTeam()
    {
        CaptureProcess = 0;
        if (_sum == 0)
        {
            IsCapture = false;
        }
        else
        {
            _lastLayerCapturing = _layerCapturing;
        }
    }
    private void EndCapture()
    {
        CaptureProcess = 0;
        IsCapture = false;
    }
    private void ResetCaptureProcess()
    {
        //if (CapturedByWhom == 0)
        //{
            _lastLayerCapturing = -1;
            _layerCapturing = -1;
        //}
        IsCapture = false;
        CaptureProcess = 0;
        _slider.ChangingValues(_maxCaptureProcess, 0);
    }
}
