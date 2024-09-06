using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Buff Management

    public int amountOfBuff;

    public void AddBuff() => amountOfBuff++;

    public void RemoveBuff() => amountOfBuff--;
    #endregion

    #region Time Management
    public int totaldays = 0;
    public int currentYear = 0;
    private int daysInYear = 365;
    private int initialDay = 1;
    public int daysPerDecision = 3;
    #endregion

    #region Stats Management
    public float publicEsteem = 50f;
    public float militaryPower = 50f;
    public float intelligence = 50f;
    public float spirituality = 50f;
    public float decisionEffectPercentage = 10f;
    #endregion
    
    public void ApplyDecisionEffect(string stat, bool isPositive) {
        float effect = decisionEffectPercentage;
        if (isPositive) effect = -effect;

        switch (stat) 
        {
            case "publicesteem":
                publicEsteem += publicEsteem * (effect / 100);
                break;
            case "militarypower":
                militaryPower += militaryPower * (effect / 100);
                break;
            case "intelligence":
                intelligence += intelligence * (effect / 100);
                break;
            case "sprirituality":
                spirituality += spirituality * (effect / 100);
                break;
        }
    }
    public void AddDaysAfterDecision() {
        totaldays += daysPerDecision;

        if (totaldays >= daysInYear) 
        {
            totaldays -= daysInYear;
            currentYear++;
        }
    }

    public void ResetDayAfterResetGame() {
        totaldays = Random.Range(1, 51);
        currentYear = 1;
    }
}
