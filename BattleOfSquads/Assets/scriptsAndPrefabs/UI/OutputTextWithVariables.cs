using UnityEngine;
using System.Collections;
using TMPro;

public class OutputTextWithVariables : MonoBehaviour
{
    [SerializeField] private TMP_Text PlaceForOutputCharacteristics;
    [SerializeField] private TMP_Text PlaceForDescription;
    [Header("The smaller the faster")]
    [SerializeField] private float _speedOutputINcharacteristics;
    [SerializeField] private float _speedOutputDescription;

    private string _text;
    private Coroutine writingCoroutine_�haracteristics;
    private Coroutine writingCoroutine_Description;
    public void Output(
        string Description,

        float Matter,
        float HP,
        float Damage,
        TypeDamage typeDamage,
        float MaxDistance,
        float MinDistance,
        float Range, 
        float Recharge, 
        float ConstructionSpeed)
    {
        _text =
            $"������� - {Matter}\n" +
            $"���������� ����� - {HP}\n";
        if (typeDamage != TypeDamage.zero)
        {
            _text += $"��������� ����� - {Damage}\n" + 
                $"��� ����� - {typeDamage}\n" + 
                $"������������ ��������� ����� - {MaxDistance}\n";
            if (MinDistance != 0) {
                _text += $"����������� ��������� ����� - {MinDistance}\n";
            }
            if (Range > 0) {
                _text += $"������� ��������� � �������� - {Range}\n";
            }
            _text += $"������������ �������� - {Recharge}\n";
        }
        if (ConstructionSpeed > 0)
        {
            _text += $"���������������� �������� - {ConstructionSpeed}\n";
        }
        Hide();
        writingCoroutine_�haracteristics = StartCoroutine(WriteText(_speedOutputINcharacteristics, _text, PlaceForOutputCharacteristics));
        writingCoroutine_Description = StartCoroutine(WriteText(_speedOutputDescription, Description, PlaceForDescription));
    }

    IEnumerator WriteText(float Speed, string Text, TMP_Text TMPText)
    {
        string currentText = "";
        for (int i = 0; i <= Text.Length; i++)
        {
            currentText = Text.Substring(0, i);
            TMPText.text = currentText;
            yield return new WaitForSeconds(Speed);
        }
    }
    public void Hide()
    {
        if (writingCoroutine_�haracteristics != null)
        {
            StopCoroutine(writingCoroutine_�haracteristics);
        }
        PlaceForOutputCharacteristics.text = "";

        if (writingCoroutine_Description != null)
        {
            StopCoroutine(writingCoroutine_Description);
        }
        PlaceForDescription.text = "";
    }
}
