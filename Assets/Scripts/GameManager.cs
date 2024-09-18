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
    public int totaldays = 0;
    public int currentYear = 0;
    public int daysInYear = 365;
    public int initialDay = 1;

    [Header("Stats")]
    public int publicEsteem = 50;
    public int militaryPower = 50;
    public int economy = 50;
    public int spirituality = 50;
    public int maxStat = 100;

    public void ApplySingleEffect(int change1, int change2, int change3, int change4) 
    {
        if(change1 !=0)
            militaryPower = Mathf.Clamp(militaryPower + change1, 0, maxStat);
        
        if(change2 != 0)
            publicEsteem = Mathf.Clamp(militaryPower + change2, 0, maxStat);

        if (change3 != 0)
            economy = Mathf.Clamp(militaryPower + change3, 0, maxStat);

        if (change4 != 0)
            spirituality = Mathf.Clamp(militaryPower + change4, 0, maxStat);
    }
    public void AddDaysAfterDecision(int rulingDays) 
    {
        totaldays += rulingDays;

        if (totaldays >= daysInYear) 
        {
            totaldays -= daysInYear;
            currentYear++;
        }
    }
}
