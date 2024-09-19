using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Buff")]

    public int amountOfBuff;
    
    public void AddBuff() => amountOfBuff++;
    
    public void RemoveBuff() => amountOfBuff--;
    
    [Header("Time")]
    public int totalDays = 0;
    public int currentYear = 0;
    public int initialDay = 1;

    [Header("Stats")]
    public int publicEsteem = 50;
    public int militaryPower = 50;
    public int economy = 50;
    public int spirituality = 50;
    public int maxStat = 100;

    public void ApplySingleEffect(int change1, int change2, int change3, int change4) 
    {
        militaryPower = Mathf.Clamp(militaryPower + change1, 0, maxStat);
        publicEsteem = Mathf.Clamp(publicEsteem + change2, 0, maxStat);
        economy = Mathf.Clamp(economy + change3, 0, maxStat);
        spirituality = Mathf.Clamp(spirituality + change4, 0, maxStat);
    }
    public void AddDaysAfterDecision(int rulingDays) 
    {
        totalDays += rulingDays;
    }
}
