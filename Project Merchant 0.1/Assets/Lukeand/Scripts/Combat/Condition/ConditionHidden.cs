using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Condition / ")]
public class ConditionHidden : Condition
{

    private void OnEnable()
    {
        conditionType = ConditionType.Hidden;

        
    }

    public override void Act(CombatSlot target, CombatSlot attacker, int damage)
    {
        //WE SIMPLY TELL THE SLOT TO BECOME OPAQUE.
        //HOW DO WE DO TO RETURN TO ITS ORIGINAL FORM?

        //AND ALSO GRANT BUFF.
        


    }
}
