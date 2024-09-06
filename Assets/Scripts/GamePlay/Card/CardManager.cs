using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public void OnCardDecision(string stat, bool isPositive) 
    {
        GameManager.Instance.ApplyDecisionEffect(stat, isPositive);
        GameManager.Instance.AddDaysAfterDecision();
    }
}
