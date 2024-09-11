using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    public void ResetDayAfterResetGame() {
        GameManager.Instance.timeSO.totaldays = Random.Range(1, 51);
        GameManager.Instance.timeSO.currentYear = 1;
    }
}
