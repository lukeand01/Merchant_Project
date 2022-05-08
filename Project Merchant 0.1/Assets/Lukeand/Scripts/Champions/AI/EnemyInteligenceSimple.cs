using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI / Simple")]
public class EnemyInteligenceSimple : EnemyInteligence
{
    //THIS FELLA SIMPLY AIMS FOR THE CLOSEST AND ATTACKS.
    //CERTAIN SIMPLE CREATURES FEAR THE POWERFUL ONES.
    public bool slavePersonality; //THIS MEANS IT WILL DO ANYTHING FOR THE STRONGEST CHARACTER IN THE ENEMYLIST.


    public override void Act(EnemyCombatSlot itself, List<AllyCombatSlot> allyList, List<EnemyCombatSlot> enemyList, List<SkillBase> skillList)
    {
        //



    }
}
