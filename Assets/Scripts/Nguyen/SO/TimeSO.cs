using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimeSO")]
public class TimeSO : ScriptableObject
{
    public int totaldays;
    public int currentYear;
    public int daysInYear;
    public int initialDay;
    public int daysPerDecision;
}
