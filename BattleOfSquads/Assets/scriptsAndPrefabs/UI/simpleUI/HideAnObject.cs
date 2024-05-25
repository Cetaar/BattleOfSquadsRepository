using UnityEngine;
public class HideAnObject : MonoBehaviour
{
    [SerializeField] GameObject _hiddenObject;
    [SerializeField] private bool _flagForHide = true;

    public void ToHideAnObject()
    {
        _flagForHide = !_flagForHide;
        _hiddenObject.SetActive(_flagForHide);
    }
}
