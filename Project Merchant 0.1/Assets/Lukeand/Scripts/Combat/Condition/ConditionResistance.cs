using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Condition / Resistance")]
public class ConditionResistance : Condition
{
    //NEGATIVE OR POSITIVE.

    //THERE IS NO SET UP OR PROC. 

    private void Awake()
    {
        conditionType = ConditionType.Resistance;
    }
    public override void SetUp(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {
        //GIVE STATS.

    }


    public override void Act( CombatSlot target, CombatSlot attacker, int damage)
    {
        //TAKE STATS.

    }

}
