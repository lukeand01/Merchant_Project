using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition / Bleed")]
public class ConditionBleed : Condition
{

    private void OnEnable()
    {
        conditionType = ConditionType.Bleed;
        damageType = DamageType.True;
    }

    public override void Act(CombatSlot target, CombatSlot attacker, int damage)
    {
        //I NEED THE SLOT.
        target.LoseHealth(damage, damageType);

    }

}
