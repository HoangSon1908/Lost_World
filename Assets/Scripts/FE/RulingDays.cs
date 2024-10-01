using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RulingDays : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rulingDayText;
    [SerializeField] private TextMeshProUGUI currentYearText;

    private int previousRulingDays = 0; 
    void Start() {
        UpdateDaysUI(); 
    }

    public void UpdateDaysUI()
    {
        int currentYear = GameManager.Instance.currentYear;
        int rulingDays = GameManager.Instance.rulingDays;

        DOTween.To(() => previousRulingDays, x => previousRulingDays = x, rulingDays, 1f) 
            .OnUpdate(() =>
            {
                int years = previousRulingDays / 365;
                int days = previousRulingDays % 365;

                int playedYear = GameManager.Instance.lastCurrentDay / 365;

                currentYearText.text = $"Năm {currentYear + playedYear}";

                if (years != 0)
                    rulingDayText.text = $"{years} năm và {days} ngày";
                else
                    rulingDayText.text = $"{days} ngày";
            })
            .OnComplete(() =>
            {
                previousRulingDays = rulingDays;
            });
    }
}
