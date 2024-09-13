using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionEffect : MonoBehaviour
{
    void Update() {
       
    }
    public void OnCardDecision(string element, bool isPositive) 
    {
        ApplySingleEffect(element, isPositive, 10);
        AddDaysAfterDecision();
    }

    public void ApplyDecisionEffect(Dictionary<string, bool> elements) 
    {
        foreach (var statEffect in elements) 
        {
            ApplySingleEffect(statEffect.Key, statEffect.Value, 10);
        }
    }
    public void ApplySingleEffect(string element, bool isPositive, int statEffect) 
    {
        float effect = statEffect;
        if (!isPositive) effect = -effect;

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
    public void AddDaysAfterDecision() 
    {
        GameManager.Instance.totaldays += GameManager.Instance.daysPerDecision;

        if (GameManager.Instance.totaldays >= GameManager.Instance.daysInYear) 
        {
            GameManager.Instance.totaldays -= GameManager.Instance.daysInYear;
            GameManager.Instance.currentYear++;
        }
    }
}
