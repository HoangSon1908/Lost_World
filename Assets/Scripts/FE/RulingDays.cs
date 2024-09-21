using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulingDays : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI rulingDayText;

    void Start() {
        UpdateDaysUI();
    }

    public void UpdateDaysUI()
    {
        int years = GameManager.Instance.rulingDays / 365;
        int days = GameManager.Instance.rulingDays % 365;

        if (years != 0)
            rulingDayText.text = $"{years} years and {days} days";
        else
            rulingDayText.text = $"{days} days";
    }
}
