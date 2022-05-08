using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteligence : ScriptableObject
{
    public virtual void Act(EnemyCombatSlot itself, List<AllyCombatSlot> allyList, List<EnemyCombatSlot> enemyList, List<SkillBase> skillList)
    {

    }
}
