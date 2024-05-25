using UnityEngine;

public class MovingObjectBackAndForth : MonoBehaviour
{
    [SerializeField] private float _offset_Y;
    [SerializeField] private GameObject[] _objects;
    private bool _flag = true;


    [SerializeField] private bool _isIU;
    public void ToMoveObjectBackAndForth()
    {
        _flag = !_flag;
        if (_flag)
        {
            OffsetItself(_offset_Y);
        }
        else
        {
            OffsetItself(-_offset_Y);
        }
    }
    private void OffsetItself(float Offset_Y)
    {
        for (int i = 0; i < _objects.Length; i++)
        {
            _objects[i].transform.Translate(0, Offset_Y, 0);
        }
    }
}
