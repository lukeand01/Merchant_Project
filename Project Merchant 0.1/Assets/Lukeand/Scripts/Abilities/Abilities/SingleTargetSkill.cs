using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Skill Base / SS")]
public class SingleTargetSkill : SkillBase
{

    public void Action(CombatSlot combatSlot)
    {
        for (int i = 0; i < conditionList.Count; i++)
        {
            combatSlot.conditionHandler.AddCondition(conditionList[i]);
        }

        if (damageType == DamageType.Heal)
        {                  
            combatSlot.RecoverHealth(strenght);
            return;
        }

        combatSlot.LoseHealth(strenght, damageType);
    }


    public override SingleTargetSkill GetSS()
    {
        return this;
    }
}
