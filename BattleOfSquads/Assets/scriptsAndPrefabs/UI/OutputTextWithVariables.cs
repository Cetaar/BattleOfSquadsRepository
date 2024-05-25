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
    private Coroutine writingCoroutine_Сharacteristics;
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
            $"Материя - {Matter}\n" +
            $"Количество жизни - {HP}\n";
        if (typeDamage != TypeDamage.zero)
        {
            _text += $"Нанесение урона - {Damage}\n" + 
                $"Тип урона - {typeDamage}\n" + 
                $"Максимальная дистанция атаки - {MaxDistance}\n";
            if (MinDistance != 0) {
                _text += $"Минимальная дистанция атаки - {MinDistance}\n";
            }
            if (Range > 0) {
                _text += $"Область поражения с радиусом - {Range}\n";
            }
            _text += $"Пререзарядка выстрела - {Recharge}\n";
        }
        if (ConstructionSpeed > 0)
        {
            _text += $"Производственные мощности - {ConstructionSpeed}\n";
        }
        Hide();
        writingCoroutine_Сharacteristics = StartCoroutine(WriteText(_speedOutputINcharacteristics, _text, PlaceForOutputCharacteristics));
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
        if (writingCoroutine_Сharacteristics != null)
        {
            StopCoroutine(writingCoroutine_Сharacteristics);
        }
        PlaceForOutputCharacteristics.text = "";

        if (writingCoroutine_Description != null)
        {
            StopCoroutine(writingCoroutine_Description);
        }
        PlaceForDescription.text = "";
    }
}
