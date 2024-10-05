using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Buff")]

    public int amountOfBuff;
    public bool seeTheFuture;
    public bool canRevive;
    public void AddBuff() => amountOfBuff++;

    public void RemoveBuff() => amountOfBuff--;

    [Header("Time")]

    public int rulingYears;
    public int rulingDays;
    public int currentYears;
    public int currentDays;

    [Header("Stats")]
    public int publicEsteem;
    public int militaryPower;
    public int economy;
    public int spirituality;
    public int maxStat;

    private bool isChecked;

    private const string PlayerPrefsDayKey = "CurrentDays";
    private const string PlayerPrefsYearKey = "CurrentYear";

    public void Start()
    {
        currentYears = PlayerPrefs.GetInt(PlayerPrefsYearKey);
        currentDays = PlayerPrefs.GetInt(PlayerPrefsDayKey);

        // Khôi phục thông tin buff từ PlayerPrefs
        seeTheFuture = PlayerPrefs.GetInt(ShopSystem.instance.prophecyEffect, 0) == 1;
        canRevive = PlayerPrefs.GetInt(ShopSystem.instance.reviveEffect, 0) == 1;

        rulingDays = Random.Range(1, 51);
        RulingDays.instance.UpdateYearsAndDaysUI(rulingDays);
        rulingYears = 0;

        isChecked = false;
    }

    public void CheckisGameOver()
    {
        CheckGameOver(militaryPower,0);
        CheckGameOver(economy,1);
        CheckGameOver(publicEsteem, 2);
        CheckGameOver(spirituality, 3);
    }

    public void ApplySingleEffect(int change1, int change2, int change3, int change4)
    {
        militaryPower = Mathf.Clamp(militaryPower + change1, 0, maxStat);
        publicEsteem = Mathf.Clamp(publicEsteem + change2, 0, maxStat);
        economy = Mathf.Clamp(economy + change3, 0, maxStat);
        spirituality = Mathf.Clamp(spirituality + change4, 0, maxStat);
    }

    private void CheckGameOver(int statValue,int DieCardIndex)
    {
        if (isChecked)
        {
            return;
        }

        if (statValue == maxStat || statValue == 0)
        {
            isChecked = true;
            if (statValue == maxStat)
            {
                DieCardIndex += 4;
            }
            if (canRevive)
            {
                canRevive = false;
                Data.instance.RevivePlayer();
                Debug.Log("Revive Player");
            }
            else
            {
                currentDays = currentDays + rulingDays - (rulingYears * 365);
                currentYears += rulingYears;
                // Lưu ngày và năm trước khi reset
                PlayerPrefs.SetInt(PlayerPrefsDayKey, currentDays);
                PlayerPrefs.SetInt(PlayerPrefsYearKey, currentYears);
                PlayerPrefs.Save();

                Debug.Log("Game Over!!");

                Choice dieCharacter = Data.instance.FindChoiceInDieCard(DieCardIndex);
                Data.instance.KillPlayer(dieCharacter);
            }
        }
    }

    public void ResetElementStats()
    {
        publicEsteem = 50;
        militaryPower = 50;
        economy = 50;
        spirituality = 50;
    }

    public void AddDaysAfterDecision(int day)
    {
        rulingDays += day;
        int Days=currentDays + rulingDays;

        // Nếu rulingDays vượt quá 365
        if (Days >= 365)
        {
            rulingYears = Days / 365;
        }

        RulingDays.instance.UpdateYearsAndDaysUI(rulingDays);
    }
}
