using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : ScriptableObject
{
    //THIS IS WHERE WE ACTUALLY DO ALL THE STUFF
    //WE NEED TO HOLD 

    public ConditionType conditionType;
    public DamageType damageType;
    

    //BUT THERE ARE CONDTIONS THAT NEED SET UP.

    public virtual void SetUp(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {

    }
    public virtual void Close(ConditionBase _base, CombatSlot target, CombatSlot attacker)
    {

    }

    public virtual void Act(CombatSlot target, CombatSlot attacker, int damage)
    {
        //WE CAN JUST SAY NULL HERE.



    }

    //OVERWATCH NEEDS TO REFERENCE A STORED COMBATSLOT TO PROTECT THE TARGET.
}
