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

                currentYearText.text = $"Year of {currentYear}";

                if (years != 0)
                    rulingDayText.text = $"{years} years and {days} days";
                else
                    rulingDayText.text = $"{days} days";
            })
            .OnComplete(() =>
            {
                previousRulingDays = rulingDays;
            });
    }
}
