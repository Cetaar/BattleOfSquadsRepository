using UnityEngine;
public class TargetPoint : MonoBehaviour, Target
{
    [SerializeField] private int _linkCount = 0;
    public void RemoveLink(GameObject Object)
    {
        _linkCount--;
        if (_linkCount <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void AddLink(GameObject Object)
    {
        _linkCount++;
    }
}
