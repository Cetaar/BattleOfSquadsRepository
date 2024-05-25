using UnityEngine;
using Cinemachine;

public class CameraControlFP : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera1;
    [SerializeField] private CinemachineVirtualCamera _camera2;
    private bool _cameraEnabled = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _camera1.enabled = _cameraEnabled;
            _cameraEnabled = !_cameraEnabled;
            //_camera2.Priority = 2;
        }
    }
}
