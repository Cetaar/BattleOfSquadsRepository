using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControlCircle : MonoBehaviour
{
    [SerializeField]
    Transform _center;

    [SerializeField]
    bool On;

    [SerializeField]
    int radius = 30;

    [SerializeField]
    float angleSpeed = 5f, SpeedUp = 1f;
    //Controls _input;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float X, Y;

    [SerializeField]
    float zoom = 0.25f;
    [SerializeField]
    float zoomMax = 10; // макс. увеличение
    [SerializeField]
    float zoomMin = 3; // мин. увеличение
    private void Start()
    {
        offset = new Vector3(offset.x, offset.y, -radius);
    }
    void Update()
    {
        On = (GlobalTime.TimeSpeed == 0);
        if (!On)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
            offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));

            Y = transform.localEulerAngles.y + Input.GetAxis("Horizontal") * angleSpeed;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, Y, transform.localEulerAngles.z);
            Vector3 newOffset = new Vector3(offset.x, transform.position.y, offset.z);
            Vector3 newPosition = transform.localRotation * newOffset;
            transform.position = new Vector3(newPosition.x, transform.position.y + Input.GetAxis("Vertical") * SpeedUp, newPosition.z);
        }
    }
}
