using UnityEngine;
using TMPro;

public class ForWarning : MonoBehaviour
{
    [SerializeField] private TMP_Text _textWarning;
    [SerializeField] private GameObject _bodyWarning;
    public float timer;
    public void WarningOutput(string TextForWarning)
    {
        _bodyWarning.SetActive(true);
        _textWarning.text = TextForWarning;
    }
}
