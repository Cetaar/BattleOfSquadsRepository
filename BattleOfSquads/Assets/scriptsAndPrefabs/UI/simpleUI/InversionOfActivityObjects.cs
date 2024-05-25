using UnityEngine;

public class InversionOfActivityObjects : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;
    private bool[] _activityObjects;
    private void Start()
    {
        _activityObjects = new bool[_objects.Length];
        for (int i = 0; i < _objects.Length; i++)
        {
            _activityObjects[i] = _objects[i].activeSelf;
        }

    }
    public void InversionOfActivity()
    {
        for(int i = 0; i < _objects.Length; i++)
        {
            _activityObjects[i] = !_activityObjects[i];
            _objects[i].SetActive(_activityObjects[i]);
        }
    }
}
