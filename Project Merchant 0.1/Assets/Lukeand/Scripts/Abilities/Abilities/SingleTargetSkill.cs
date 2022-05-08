using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Skill Base / SS")]
public class SingleTargetSkill : SkillBase
{
    
    //YOU SELECT ONE BUT THE REST IS RANDOM.
 

    public void Action(CombatSlot target, CombatSlot attacker)
    {            
         SingleTarget(target, attacker);
             
    }

    void SingleTarget(CombatSlot target, CombatSlot attacker)
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            target.conditionHandler.AddCondition(conditionList[i], attacker);
        }

        if (damageType == DamageType.Heal)
        {
            target.RecoverHealth(strenght);
            return;
        }

        if (damageType != DamageType.True)
        {
            int damage = Utils.GetDamage(attacker);
            int damageAfterMitigation = Utils.GetResistance(damage, damageType, attacker);
            target.LoseHealth(damageAfterMitigation, damageType);
            return;
        }

        target.LoseHealth(strenght, damageType);
    }

    

    public override SingleTargetSkill GetSS()
    {
        return this;
    }
}
