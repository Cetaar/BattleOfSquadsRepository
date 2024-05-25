using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSlant : MonoBehaviour
{
    [SerializeField]
    float SpeedUpDown = 1f, X, Y, noD = 0.8f;
    [SerializeField]
    float limit = 90;
    [SerializeField]
    bool On;
    void Start()
    {
        X = transform.localEulerAngles.x;
        Y = transform.localEulerAngles.y;
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90;
    }
    void Update()
    {
        On = (GlobalTime.TimeSpeed == 0);
        if (On)
        {
            if (Input.GetMouseButton(2)) if (Mathf.Abs(Input.GetAxis("Mouse Y")) > noD)
                {
                    X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * SpeedUpDown;
                    Y += Input.GetAxis("Mouse Y") * SpeedUpDown;
                    Y = Mathf.Clamp(Y, -limit, limit);
                    transform.localEulerAngles = new Vector3(-Y, X, 0);
                }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.X))
            {
                transform.localEulerAngles = Vector3.zero;
                X = 0;
                Y = 0;
            }
        }
    }
}
