using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _speedRot;
    [SerializeField] private float _indexSmoothly = 0.1f;
    [SerializeField] private float _speed;

    [SerializeField] private float _xRot;
    [SerializeField] private float _yRot;

    private float _xCurRot;
    private float _yCurRot;
    private float _xCurV;
    private float _yCurV;

    private float _transX;
    private float _transZ;

    private Transform _father;
    private void Start()
    {
        _father = CameraMoveDefault.instance.transform;
    }
    public void RotationCamera()
    {
        _xRot -= Input.GetAxis("Mouse Y") * _speedRot;
        _yRot += Input.GetAxis("Mouse X") * _speedRot;

        _xRot = Mathf.Clamp(_xRot, -90, 90);

        _xCurRot = Mathf.SmoothDamp(_xCurRot, _xRot, ref _xCurV, _indexSmoothly);
        _yCurRot = Mathf.SmoothDamp(_yCurRot, _yRot, ref _yCurV, _indexSmoothly);

        gameObject.transform.rotation = Quaternion.Euler(_xCurRot, _yCurRot, 0);
    }
    public void TranslateCamera()
    {
        _transX = Input.GetAxis("Horizontal") * _speed;
        _transZ = Input.GetAxis("Vertical") * _speed;
        transform.Translate(_transX, 0, _transZ);

        transform.SetParent(null);
        _father.position = transform.position;
        transform.SetParent(_father);
    }
    public void ZomeCamera(float Zoom)
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            transform.Translate(0, 0, -Zoom);// += Vector3.back * Zoom;

        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            transform.Translate(0, 0, Zoom); //position += Vector3.forward * Zoom;
        }

        transform.SetParent(null);
        _father.position = transform.position;
        transform.SetParent(_father);
    }
}
