using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Buff")]

    public int amountOfBuff;
    public bool seeTheFuture;
    public void AddBuff() => amountOfBuff++;
    
    public void RemoveBuff() => amountOfBuff--;
    
    [Header("Time")]
    public int rulingDays;
    public int currentYear;
    public int rulingYears;

    [Header("Stats")]
    public int publicEsteem;
    public int militaryPower;
    public int economy;
    public int spirituality;
    public int maxStat;

    public void CheckisGameOver()
    {
        CheckGameOver(militaryPower);
        CheckGameOver(economy);
        CheckGameOver(publicEsteem);
        CheckGameOver(spirituality);
    }
    public void ApplySingleEffect(int change1, int change2, int change3, int change4) 
    {
        militaryPower = Mathf.Clamp(militaryPower + change1, 0, maxStat);
        publicEsteem = Mathf.Clamp(publicEsteem + change2, 0, maxStat);
        economy = Mathf.Clamp(economy + change3, 0, maxStat);
        spirituality = Mathf.Clamp(spirituality + change4, 0, maxStat);
    }
    public void AddDaysAfterDecision(int day) 
    {
        rulingDays += day;
    }

    private void CheckGameOver(int statValue) {
        if (statValue == maxStat || statValue == 0) {
            ResetDayAfterResetGame();
            ResetElementStats();
            //ResetCard();
            StatManager.instance.ApplyStatChanges();
            Debug.Log("Game Over!!");
        }
    }

    public void ResetDayAfterResetGame() {
        rulingDays = Random.Range(1, 51);
        rulingYears = 1;
    }

    public void ResetElementStats() {
        publicEsteem = 50;
        militaryPower = 50;
        economy = 50;
        spirituality = 50;
    }
}
