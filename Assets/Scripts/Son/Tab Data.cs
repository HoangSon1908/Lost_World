using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabData : MonoBehaviour
{
    //singleton pattern
    public static TabData instance;

    //bool for Revive
    [HideInInspector]
    public bool canRevive;
    [HideInInspector]
    public bool seeTheFuture;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canRevive = PlayerPrefs.GetInt(ShopSystem.instance.reviveEffect, 0) == 1;
        seeTheFuture = PlayerPrefs.GetInt(ShopSystem.instance.prophecyEffect, 0) == 1;
    }
}
