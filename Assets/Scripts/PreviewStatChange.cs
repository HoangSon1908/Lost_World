using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewStatChange : MonoBehaviour
{
    private Color defaultColor = Color.white;
    private Color positiveEffectColor = Color.green;
    private Color negativeEffectColor = Color.red;

    public IEnumerator FlickerStatChange(Image statBar, int statEffect) {
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
}
