using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEffect : MonoBehaviour
{
    public void OnCardDecision(string stat, bool isPositive) 
    {
        ApplySingleEffect(stat, isPositive);
        AddDaysAfterDecision();
    }

    public void ApplyDecisionEffect(Dictionary<string, bool> stats) 
    {
        foreach (var statEffect in stats) 
        {
            ApplySingleEffect(statEffect.Key, statEffect.Value);
        }
    }
    public void ApplySingleEffect(string stat, bool isPositive) 
    {
        float effect = GameManager.Instance.statsSO.decisionEffectPercentage;
        if (!isPositive) effect = -effect;

        switch (stat) 
        {
            case "publicesteem":
                GameManager.Instance.statsSO.publicEsteem += GameManager.Instance.statsSO.publicEsteem * (effect / 100);
                break;
            case "militarypower":
                GameManager.Instance.statsSO.militaryPower += GameManager.Instance.statsSO.militaryPower * (effect / 100);
                break;
            case "intelligence":
                GameManager.Instance.statsSO.intelligence += GameManager.Instance.statsSO.intelligence * (effect / 100);
                break;
            case "sprirituality":
                GameManager.Instance.statsSO.spirituality += GameManager.Instance.statsSO.spirituality * (effect / 100);
                break;
        }
    }
    public void AddDaysAfterDecision() 
    {
        GameManager.Instance.timeSO.totaldays += GameManager.Instance.timeSO.daysPerDecision;

        if (GameManager.Instance.timeSO.totaldays >= GameManager.Instance.timeSO.daysInYear) 
        {
            GameManager.Instance.timeSO.totaldays -= GameManager.Instance.timeSO.daysInYear;
            GameManager.Instance.timeSO.currentYear++;
        }
    }
}
