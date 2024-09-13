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
    public int daysPerDecision = 3;

    [Header("Stats")]
    public float publicEsteem = 50f;
    public float militaryPower = 50f;
    public float economy = 50f;
    public float spirituality = 50f;
}
