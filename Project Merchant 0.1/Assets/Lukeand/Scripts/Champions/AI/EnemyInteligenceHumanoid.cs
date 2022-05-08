using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI / Humanoid")]
public class EnemyInteligenceHumanoid : EnemyInteligence
{
    //THEY THINK MORE.
    //IS HE SELFISH.
    //



    public override void Act(EnemyCombatSlot itself, List<AllyCombatSlot> allyList, List<EnemyCombatSlot> enemyList, List<SkillBase> skillList)
    {
        bool firstChoice = false;
        SkillBase chosenSkill = null;


        //I SHOULD BE ABLE TO USE THE SAME ATTACK. IF NOT SILENCED.
        //BUT I SHOUDL BE CAREFUL TO TAKE AN ABILITY THAT HAS NO POSSIBLE TARGET.

        if (itself.conditionHandler.HasCondition(ConditionType.Confused))
        {
            //THIS MEANS THAT HE WILL ATTACK AN ENEMY.
        }

        if (itself.conditionHandler.HasCondition(ConditionType.Manipulated))
        {
            //THIS MEANS HE EITHER HEALS AN ALLY OR ATTACKS AN ENEMY
        }

        while (!firstChoice)
        {
            int firstSelection = Random.Range(0, skillList.Count - 1);
            chosenSkill = skillList[firstSelection];

            //I ALSO WANT TO KNOW IF THERE ARE TARGETS. THERE IS ALWAYS TARGETS FOR ATTACKS.

            //ALSO IF YOU HAVE HEAL BUT NO ONE TO HEAL.
            if(chosenSkill.damageType == DamageType.Heal)
            {
                
                //THEN WE CHECK IF WE HAVE HEALED TARGETS.
                if(Utils.GetLowestHealthEnemy(enemyList) == null)
                {
                    //NO TARGETS WHO NEED HEALING. CHOOSE ANOTHER ABILITY.
                    return;
                }
                else
                {
                    Debug.Log("there is a target for healing");
                }
                
            }


            if (itself.conditionHandler.HasCondition(ConditionType.Silence))
            {

                if(chosenSkill.skillName == itself.lastSkill.skillName)
                {
                    //THEN WE TRY THIS SHIT AGAIN.
                    return;
                }

            }


            Debug.Log("this skill is the right one");
            firstChoice = true;
        }

        if (chosenSkill.targetType == TargetType.Self)
        {
            //THEN WE MUST CHECK IF WE SHOULD DO THIS.
            itself.SufferAction(chosenSkill, itself);
        }
        if (chosenSkill.targetType == TargetType.Enemy)
        {           
            AttackingEnemy(chosenSkill, allyList, itself);
        }
        if (chosenSkill.targetType == TargetType.Ally)
        {
            //THEN WE DO SOMETHING TOWARDS AN ALLY.
            HelpingAlly(itself, chosenSkill, enemyList);
        }
    }


    void AttackingEnemy(SkillBase skill, List<AllyCombatSlot> allyList, EnemyCombatSlot itself)
    {
        //I THINK YOU SHOULD JUST AIM WHATEVER.
        //IF YOU HAVE A RANGED ATTACK. YOU MUST ATTACK THOSE IN RANGE.

        //ATTACK A TARGET. 
        //WHAT IF THE ONLY THING HAS HIM FEARED?
        //WHAT IF THE TARGET IS DEAD?


        List<AllyCombatSlot> newList = Utils.CreateTargettableAllyList(skill, allyList);
        int decision = Random.Range(0, newList.Count - 1);
        AllyCombatSlot target = newList[decision];
        target.SufferAction(skill, itself);

    }

    void HelpingAlly(EnemyCombatSlot itself, SkillBase skill, List<EnemyCombatSlot> list)
    {
        
       //THERE ARE TARGETS BECAUSE I ALREADY CHECK.
       //FOR NOW ONLY HEAL WILL GET HERE.
        if(skill.damageType == DamageType.Heal)
        {
            //THEN WE HEAL THE LOWEST HEALTH TARGET.
            Utils.GetLowestHealthEnemy(list).SufferAction(skill, itself);
            return;
        }

        
        
        //RESISTANCE SHOULD BE GIVEN TO FRONTLINE AND DAMAGE TO THE BACKLINE.





        

    }



    EnemyCombatSlot TargetNeedHealth(List<EnemyCombatSlot> enemyList)
    {
        //WE LOOK FOR A FELLA THAS LOST A PORCENTAGE OF ITS HEALTH.
        //ALSO HAVE TO CHECK PERSONALITY BECAUSE IN THAT CASE THEY WILL FAVOR OTHER PEOPLE.

        EnemyCombatSlot newSlot = null;

        for (int i = 0; i < enemyList.Count; i++)
        {

        }


        return newSlot;
    }
}
