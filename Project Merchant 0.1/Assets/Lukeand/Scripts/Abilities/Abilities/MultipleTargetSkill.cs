using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill Base / MS")]
public class MultipleTargetSkill : SkillBase
{
   //THIS IS ONE YUO HAVE TO SELECT ALL AND THEN SEND THE INFO HERE BY THE LAST.


    
    void Action()
    {

    }

    void MultipleRandomAction()
    {


    }

    void MultipleSelectedAction(List<CombatSlot> targets, CombatSlot attacker)
    {
        //



    }




    public override MultipleTargetSkill GetMS()
    {
        return this;
    }

}
