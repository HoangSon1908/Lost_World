using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private ResetGame resetGame;

    void Awake() {
        if (resetGame == null) 
        {
            resetGame = GetComponent<ResetGame>();
        }
    }

    void Update() {
    /* 
    If card is swiped right, then do:
        decisionEffect.OnCardDecision(GameManager.Instance.StatsSO.affectedStats, isPositive);
    If card is swiped left, then do:
        decisionEffect.OnCardDecision(GameManager.Instance.StatsSO.affectedStats, !isPositive);
    */
    }
}
