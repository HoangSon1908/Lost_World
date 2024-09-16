using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEffect : MonoBehaviour
{
    public void OnCardDecision(string element, int statEffect, int rulingDays) 
    {
        ApplySingleEffect(element, statEffect);
        AddDaysAfterDecision(rulingDays);
    }

    public void ApplySingleEffect(string element, int statEffect) 
    {
        float effect = statEffect;
        switch (element) 
        {
            case "publicesteem":
                GameManager.Instance.publicEsteem += effect;
                break;
            case "militarypower":
                GameManager.Instance.militaryPower += effect;
                break;
            case "economy":
                GameManager.Instance.economy += effect;
                break;
            case "sprirituality":
                GameManager.Instance.spirituality += effect;
                break;
        }
    }
    public void AddDaysAfterDecision(int rulingDays) 
    {
        GameManager.Instance.totaldays += rulingDays;

        if (GameManager.Instance.totaldays >= GameManager.Instance.daysInYear) 
        {
            GameManager.Instance.totaldays -= GameManager.Instance.daysInYear;
            GameManager.Instance.currentYear++;
        }
    }
}
