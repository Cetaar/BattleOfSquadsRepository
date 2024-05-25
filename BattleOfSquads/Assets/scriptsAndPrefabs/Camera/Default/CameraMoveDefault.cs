using UnityEngine;

public class CameraMoveDefault : MonoBehaviour
{
    [SerializeField] private CameraRotation _cameraRotation;

    public static CameraMoveDefault instance;

    //[SerializeField] private float _CamMax;
    //[SerializeField] private float _CamMin;

    private float _transX;
    private float _transZ;
    [SerializeField] private float _speed = 2.5f;

    [SerializeField] private float Zoom = 0.1f;

    float _Oldspeed = 2.5f;
    bool BoostFlag = true;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _Oldspeed = _speed;
    }
    public void SpeedBoost()
    {
        if (BoostFlag)
        {
            _speed = 3 * _speed;
            BoostFlag = false;
        }
        else
        {
            _speed = _Oldspeed;
            BoostFlag = true;
        }
    }
    public void TranslateCamera2()
    {
        _cameraRotation.TranslateCamera();
    }
    public void RotationCamera()
    {
        _cameraRotation.RotationCamera();
    }
    public void ZomeCamera()
    {
        _cameraRotation.ZomeCamera(Zoom);
    }
    public void TranslateCamera()
    {
        _transX = Input.GetAxis("Horizontal") * _speed;
        _transZ = Input.GetAxis("Vertical") * _speed;
        gameObject.transform.Translate(_transX, 0, _transZ);
    }
}
