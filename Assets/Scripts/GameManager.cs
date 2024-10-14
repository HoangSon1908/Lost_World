using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [Header("Time")]

    public int rulingYears;
    public int rulingDays;
    public int currentYears;
    public int currentDays;
    public List<int> topRulingDays = new List<int>();

    [Header("Stats")]
    public int publicEsteem;
    public int militaryPower;
    public int economy;
    public int spirituality;
    public int maxStat;
    private int[] Stat;

    public bool isChecked;


    private const string PlayerPrefsDayKey = "CurrentDays";
    private const string PlayerPrefsYearKey = "CurrentYear";
    public void Start()
    {
        isChecked = false;
        currentYears = PlayerPrefs.GetInt(PlayerPrefsYearKey);
        currentDays = PlayerPrefs.GetInt(PlayerPrefsDayKey);

        //rulingDays = Random.Range(1, 51);
        rulingYears = 0;
        RulingDays.instance.UpdateYearsAndDaysUI(rulingDays);
        
    }

    public void ApplySingleEffect(int change1, int change2, int change3, int change4)
    {
        militaryPower = Mathf.Clamp(militaryPower + change1, 0, maxStat);
        publicEsteem = Mathf.Clamp(publicEsteem + change2, 0, maxStat);
        economy = Mathf.Clamp(economy + change3, 0, maxStat);
        spirituality = Mathf.Clamp(spirituality + change4, 0, maxStat);
    }

    public void CheckGameOver()
    {
        if (isChecked)
        {
            return;
        }

        // Mảng chứa các chỉ số
        Stat = new int[] { militaryPower, publicEsteem, economy, spirituality };

        // Mảng chứa trạng thái buff tương ứng
        bool[] buffs = new bool[]
        {
        Data.instance.hasMilitaryBuff,
        Data.instance.hasPublicEsteemBuff,
        Data.instance.hasEconomyBuff,
        Data.instance.hasSpiritualityBuff
        };

        // Mảng chứa key của các buff để xoá buff khi cần
        string[] buffKeys = new string[]
        {
        Data.instance.hasMilitaryBuffKey,
        Data.instance.hasPublicEsteemBuffKey,
        Data.instance.hasEconomyBuffKey,
        Data.instance.hasSpiritualityBuffKey
        };

        // Mảng chứa loại buff tương ứng với chỉ số
        BuffType[] buffTypes = new BuffType[]
        {
        BuffType.military,
        BuffType.publicEsteem,
        BuffType.economy,
        BuffType.spirituality
        };

        int DieCardIndex = 0;

        for (int i = 0; i < Stat.Length; i++)
        {
            int statValue = Stat[i];

            if (statValue == maxStat || statValue == 0)
            {
                if (buffs[i])
                {
                    // Xoá buff tương ứng
                    buffs[i] = false;
                    PlayerPrefs.SetInt(buffKeys[i], 0); // Xóa buff
                    PlayerPrefs.Save();

                    switch (i)
                    {
                        case 0:
                            militaryPower = 50;
                            Data.instance.SetUpBuff(i, BuffType.military);
                            break;
                        case 1:
                            publicEsteem = 50;
                            Data.instance.SetUpBuff(i, BuffType.publicEsteem);
                            break;
                        case 2:
                            economy = 50;
                            Data.instance.SetUpBuff(i, BuffType.economy);
                            break;
                        case 3:
                            spirituality = 50;
                            Data.instance.SetUpBuff(i, BuffType.spirituality);
                            break;
                    }
                }
                if (statValue == maxStat)
                {
                    DieCardIndex += 4;
                }

                currentDays = currentDays + rulingDays - (rulingYears * 365);
                currentYears += rulingYears;

                // Lưu ngày và năm trước khi reset
                PlayerPrefs.SetInt(PlayerPrefsDayKey, currentDays);
                PlayerPrefs.SetInt(PlayerPrefsYearKey, currentYears);
                PlayerPrefs.Save();
                Debug.Log("Game Over!!");

                // Tìm và giết nhân vật dựa trên DieCardIndex
                Choice dieCharacter = Data.instance.FindChoiceInDieCard(DieCardIndex);
                Data.instance.KillPlayer(dieCharacter);
            
                isChecked = true;

                // Thoát khỏi vòng lặp khi phát hiện giá trị game over
                break;

            }
            DieCardIndex++;
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
        int Days = currentDays + rulingDays;

        // Nếu rulingDays vượt quá 365
        if (Days >= 365)
        {
            rulingYears = Days / 365;
        }

        RulingDays.instance.UpdateYearsAndDaysUI(rulingDays);
    }
}