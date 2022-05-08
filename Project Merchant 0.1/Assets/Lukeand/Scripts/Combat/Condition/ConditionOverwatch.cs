using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition / Overwatch")]
public class ConditionOverwatch : Condition
{
    private void OnEnable()
    {
        conditionType = ConditionType.Overwatch;
    }
    public override void SetUp(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {
        //I DONT CALL IT HERE.
    }

    public override void Act(CombatSlot target, CombatSlot attacker, int damage)
    {
        
    }
}
