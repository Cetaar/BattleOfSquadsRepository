using UnityEngine;

public class ForBodyWarning : MonoBehaviour
{
    private float _timer;

    private void OnEnable()
    {
        _timer = transform.GetComponentInParent<ForWarning>().timer;
    }
    private void Update()
    {
        ReportBeforeEnd();
    }
    private void ReportBeforeEnd()
    {
        _timer -= Time.deltaTime;
        if (_timer < 0)
        {
            End();
        }
    }
    private void End()
    {
        gameObject.SetActive(false);
    }
}
