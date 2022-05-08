using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Condition / Hidden")]
public class ConditionHidden : Condition
{

    private void OnEnable()
    {
        conditionType = ConditionType.Hidden;

        
    }

    public override void SetUp(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {
        Image targetImage = target.portrait.GetComponent<Image>();

        Color tempColor = targetImage.color;
        tempColor.a = 0.5f;

        targetImage.color = tempColor;
    }

    public override void Close(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {
        //THIS IS CALLED WHEN WE REMOVE THE CONDITION.
        Image targetImage = target.portrait.GetComponent<Image>();

        var tempColor = targetImage.color;
        tempColor.a = 1f;

        targetImage.color = tempColor;
    }

}
