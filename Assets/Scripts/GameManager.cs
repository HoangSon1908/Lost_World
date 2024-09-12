using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public StatsSO statsSO;
    public TimeSO timeSO;
    public int amountOfBuff;
    public void AddBuff() => amountOfBuff++;
    public void RemoveBuff() => amountOfBuff--;
}
