using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance {get; private set;}

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        } 
        else 
        {
            Destroy(gameObject);
        }
    }
    public void SavePreGameOverState() 
    {
        PlayerPrefs.SetInt("LastCurrentDay", GameManager.Instance.lastCurrentDay);

        /*PlayerPrefs.SetInt("MilitaryStat", GameManager.Instance.militaryPower);
        PlayerPrefs.SetInt("EconomyStat", GameManager.Instance.economy);
        PlayerPrefs.SetInt("PublicEsteemStat", GameManager.Instance.publicEsteem);
        PlayerPrefs.SetInt("SpiritualityStat", GameManager.Instance.spirituality);*/

        PlayerPrefs.Save();
    }

    public void LoadLastState()
    {
        GameManager.Instance.lastCurrentDay = PlayerPrefs.GetInt("LastCurrentDay");

        /*GameManager.Instance.militaryPower = PlayerPrefs.GetInt("MilitaryStat");
        GameManager.Instance.economy = PlayerPrefs.GetInt("EconomyStat");
        GameManager.Instance.publicEsteem = PlayerPrefs.GetInt("PublicEsteemStat");
        GameManager.Instance.spirituality = PlayerPrefs.GetInt("SpiritualityStat");*/

        //StatManager.instance.ApplyStatChanges();
    }
}
