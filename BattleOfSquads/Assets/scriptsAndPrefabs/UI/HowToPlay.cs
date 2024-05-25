using UnityEngine;
using TMPro;
public class HowToPlay : MonoBehaviour
{
    private bool flag = false;
    [SerializeField] private TMP_Text TextForMy;
    private string PrimoryTextForMy;
    [SerializeField] private string SecondoryTextForMy;
    private void Start()
    {
        PrimoryTextForMy = TextForMy.text;
    }
    public void OnButtonDown()
    {
        flag = !flag;
        if (flag == true)
        {
            TextForMy.text = SecondoryTextForMy;
        }
        else
        {
            TextForMy.text = PrimoryTextForMy;
        }
    }
}
