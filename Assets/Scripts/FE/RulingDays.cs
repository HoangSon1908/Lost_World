﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class RulingDays : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rulingDayText;
    [SerializeField] private TextMeshProUGUI currentYearText;

    public static RulingDays instance;

    private int previousRulingDays = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateYearsAndDaysUI(int targetDays)
    {
        int currentYears =GameManager.Instance.currentYears;
        int rulingYears = GameManager.Instance.rulingYears;

        DOTween.To(() => previousRulingDays, x => previousRulingDays = x, targetDays, 1f)
            .OnUpdate(() =>
            {
                // Update year text
                currentYearText.text = $"Năm {currentYears+rulingYears}";

                // Calculate the ruling years and display ruling days incrementally
                if (previousRulingDays > 365)
                    rulingDayText.text = $"{previousRulingDays / 365} năm và {previousRulingDays%365} ngày";
                else
                    rulingDayText.text = $"{previousRulingDays} ngày";
            })
            .OnComplete(() =>
            {
                previousRulingDays = targetDays; // Finalize the value
            });
    }
}
