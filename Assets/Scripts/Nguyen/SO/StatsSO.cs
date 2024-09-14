using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StatsSO")]
public class StatsSO : ScriptableObject
{
    public float publicEsteem;
    public float militaryPower;
    public float intelligence;
    public float spirituality;
    public string firstStat;
    public string secondStat;
    public string thirdStat;
    public string fouthStat;
    public float decisionEffectPercentage;
}
