using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    private Color defaultColor = Color.white;
    private Color positiveEffectColor = Color.green;
    private Color negativeEffectColor = Color.red;
    public Stat stat1Bar;
    public Stat stat2Bar;
    public Stat stat3Bar;
    public Stat stat4Bar;
    public static StatManager instance { get; private set; }

    void Awake() 
    {
        if (instance == null) 
        {
            instance = this;
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    void Start() {
        ApplyStatChanges();
    }

    /*public void PreviewStatChange(int decisionType) 
    {
        if (decisionType == 1) 
        {
            Choice choice = Data.instance.CurrentChoice;
            if (choice.militaryEffect1 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.militaryEffect1));
            if (choice.publicEsteem1 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.publicEsteem1));
            if (choice.economy1 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.economy1));
            if (choice.spiritualityEffect1 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.spiritualityEffect1));
        }
        else 
        {
            Choice choice = Data.instance.CurrentChoice;
            if (choice.militaryEffect2 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.militaryEffect2));
            if (choice.publicEsteem2 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.publicEsteem2));
            if (choice.economy2 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.economy2));
            if (choice.spiritualityEffect2 != 0) 
                StartCoroutine(FlickerStatChange(Data.instance.militaryPowerStatBar, choice.spiritualityEffect2));
        }
    }*/
    public IEnumerator FlickerStatChange(Image statBar, int statEffect) 
    {
        Color flickerColor = (statEffect > 0) ? positiveEffectColor : negativeEffectColor;
        float flickDuration = 0.5f;
        int flickCount = 3;

        for (int i = 0; i < flickCount; i++) 
        {
            statBar.color = flickerColor;
            yield return new WaitForSeconds(flickDuration / 2);
            statBar.color = defaultColor;
            yield return new WaitForSeconds(flickDuration / 2);
        }

        statBar.color = defaultColor;
    }

    public void ApplyStatChanges() {
        stat1Bar.UpdateStatBar(GameManager.Instance.militaryPower, GameManager.Instance.maxStat);
        stat2Bar.UpdateStatBar(GameManager.Instance.publicEsteem, GameManager.Instance.maxStat);
        stat3Bar.UpdateStatBar(GameManager.Instance.economy, GameManager.Instance.maxStat);
        stat4Bar.UpdateStatBar(GameManager.Instance.spirituality, GameManager.Instance.maxStat);
    }
}
