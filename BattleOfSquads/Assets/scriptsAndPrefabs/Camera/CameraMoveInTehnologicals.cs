using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveInTehnologicals : MonoBehaviour
{
    [SerializeField]
    float zoom = 0.25f;
    [SerializeField]
    float zoomMax = 10; // макс. увеличение
    [SerializeField]
    float zoomMin = 3; // мин. увеличение
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    float Speed = 1f;

    [SerializeField]
    int limidUpDawn = 50;
    [SerializeField]
    int limidLeftRight = 100;

    //[SerializeField] private float PlusRestrictions;

    void Start()
    {
        offset = transform.position;
    }
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom;
        offset.z = Mathf.Clamp(offset.z, -Mathf.Abs(zoomMax), -Mathf.Abs(zoomMin));


        if (Input.GetButton("Fire1"))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                if (offset.x - Input.GetAxis("Mouse X") * Speed >= -limidLeftRight) offset.x -= Input.GetAxis("Mouse X") * Speed;
            }
            else
            {
                if (offset.x - Input.GetAxis("Mouse X") * Speed <= limidLeftRight) offset.x -= Input.GetAxis("Mouse X") * Speed;
            }
            if(Input.GetAxis("Mouse Y") > 0)
            {
                if (offset.y - Input.GetAxis("Mouse Y") * Speed >= -limidUpDawn) offset.y -= Input.GetAxis("Mouse Y") * Speed;
            }
            else
            {
                if (offset.y - Input.GetAxis("Mouse Y") * Speed <= limidUpDawn) offset.y -= Input.GetAxis("Mouse Y") * Speed;
            }
        }
        transform.position = offset;
    }
}
