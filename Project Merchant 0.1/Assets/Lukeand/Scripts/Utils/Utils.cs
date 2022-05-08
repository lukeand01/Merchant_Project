using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils 
{
    //THIS EXISTS JUST TO HELP WITH STUFF THAT I DONT WANT TO REPEAT


    #region GET BASE LISTS
    public static Dictionary<PlayerStats, int> GetBasePlayerStats()
    {
        Dictionary<PlayerStats, int> newList = new Dictionary<PlayerStats, int>();

        newList.Add(PlayerStats.Speech, 0);
        newList.Add(PlayerStats.Barter, 0);
        newList.Add(PlayerStats.Logistic, 0);
        newList.Add(PlayerStats.Medicine, 0);
        newList.Add(PlayerStats.Wisdom, 0);
        newList.Add(PlayerStats.Luck, 0);

        return newList;
    }

    public static List<CharacterStatsList> GetCharacterBaseList()
    {
        List<CharacterStatsList> statList = new List<CharacterStatsList>();

        CharacterStatsList healthStat = new CharacterStatsList();
        healthStat.stat = ChampionStat.Health;
        statList.Add(healthStat);

        CharacterStatsList resourceStat = new CharacterStatsList();
        resourceStat.stat = ChampionStat.Resource;
        statList.Add(resourceStat);

        CharacterStatsList physicDamStat = new CharacterStatsList();
        physicDamStat.stat = ChampionStat.PhysicalDamage;
        statList.Add(physicDamStat);

        CharacterStatsList magicalDamStat = new CharacterStatsList();
        magicalDamStat.stat = ChampionStat.MagicDamage;
        statList.Add(magicalDamStat);

        CharacterStatsList tenacityStat = new CharacterStatsList();
        tenacityStat.stat = ChampionStat.Tenacity;
        statList.Add(tenacityStat);

        CharacterStatsList agilityStat = new CharacterStatsList();
        agilityStat.stat = ChampionStat.Agility;
        statList.Add(agilityStat);

        CharacterStatsList critChanceStat = new CharacterStatsList();
        critChanceStat.stat = ChampionStat.CritChance;
        statList.Add(critChanceStat);

        CharacterStatsList critDamageStat = new CharacterStatsList();
        critDamageStat.stat = ChampionStat.CritDamage;
        statList.Add(critDamageStat);

        return statList;

    }

    public static List<NpcListSprite> GetBaseNpcSpriteList()
    {
        List<NpcListSprite> newList = new List<NpcListSprite>();
        List<string> baseEmotions = new List<string>();
        baseEmotions.Add("normal");
        baseEmotions.Add("happy");
        baseEmotions.Add("sad");
        baseEmotions.Add("angry");
        baseEmotions.Add("wounded");


        for (int i = 0; i < baseEmotions.Count; i++)
        {
            NpcListSprite unit = new NpcListSprite();

            unit.potraitName = baseEmotions[i];

            newList.Add(unit);

        }

        return newList;

    }

  

    #endregion


    #region NARRATIONS

    //I WILL HAVE TO VASTLY IMPROVE THE NARRATION CREATORS.


    public static List<string> PresentationNarration() //I NEED VARIABLES LATER ON.
    {
        List<string> newList = new List<string>();

        newList.Add("This is presentation");

        newList.Add("Battle will start");


        return newList;

    }

    public static string AdditionalDescription(SkillBase skill)
    {
        //THIS IS MEANT TO DESCRIBER THE DAMAGE AND TYPE OF THE ABILITY DESCRIPTION
        string newString = "";
        string actionVerb = "";
        string damageType = "";

        actionVerb = "Deals";
        damageType = skill.damageType.ToString();

        if(skill.damageType == DamageType.Heal)
        {
            actionVerb = "Heals";
            damageType = "";
        }



        newString = $"{skill.skillName} {actionVerb} {skill.strenght} {damageType}. ";


        for (int i = 0; i < skill.conditionList.Count; i++)
        {
           // newString += $"{skill.skillName} causes {skill.conditionList[i].type} with strenght {skill.conditionList[i].strenght} ";
        }

        return newString;

    }

    public static List<string> EnemyTurnNarration(EnemyCombatSlot enemy)
    {
        List<string> newList = new List<string>();

        //WE SHOW CONDITIONS HERE.

        string firstLine = "";


        firstLine = $"it is {enemy.slotName} turn now";



        newList.Add(firstLine);

        return newList;


    }

    public static List<string> CombatNarration(SkillBase skill, CombatSlot target, CombatSlot attacker)
    {
        List<string> newList = new List<string>();
        string mainString = "";


        mainString = $"{attacker.slotName} used {skill.skillName}. {target.slotName} received {skill.strenght}";
        newList.Add(mainString);


        return newList;

    }

    public static List<string> CCNarration(CombatSlot target)
    {
        List<string> newList = new List<string>();

        string mainText = "";

        mainText = $"{target.slotName} is Stunned";

        newList.Add(mainText);

        return newList;

    }

    public static List<string> ConditionNarration(ConditionBase condition, CombatSlot target)
    {
        List<string> newList = new List<string>();

        string mainText = "";

        mainText = $"{target.slotName} suffered {condition._condition.conditionType} for {condition.strenght} {condition._condition.damageType} damage";

        newList.Add(mainText);

        return newList;

    }


    #endregion


    #region COMBAT

    public static int DamageMitigationCalculation(int oy, CombatSlot target, CombatSlot attacker)
    {
        //WE HAVE TO SEE WHAT KIND OF DAMAGE IT IS.
        //WE HAVE TO SEE ATTACKER PENETRATION.
        //WE HAVE TO SEE TARGET RESISTANCE.





        return 0;

    }

    public static Sprite GetConditionSprite(string condition)
    {
        //
        Sprite newSprite = Resources.Load<Sprite>($"ConditionFolder / {condition}");


        return newSprite;
    }

    public static int GetDamage(CombatSlot user)
    {
        //WE CHECK IF WE CRIT HERE.
        //WE ALSO ADD BUFFS AFTER THE CRIT.

        //CRIT CHANCE WORK BY X OUT OF 100.





        int newDamage = 0;

        int baseDamageScale = (int)(user.currentSkill.levelScaling * user.currentLevel);
        int baseDamage = user.currentSkill.strenght + baseDamageScale;

        int critChance = user.critChance;
        int critNumber = Random.Range(0, 100);

        if(critChance >= critNumber)
        {
            //WE INCREASE THE DAMAGE BY THE CRITDAMAGE 

            newDamage *= user.critDamage;

        }

        List<ConditionBase> damageBuffList = GetBuffDamageConditions(user.conditionHandler.conditionList);
        //WHAT IF ITS PORCENTAGE?
        int buffDamage = 0;

        for (int i = 0; i < damageBuffList.Count; i++)
        {
            if (damageBuffList[i].isPorcentage)
            {
                //
                int porcentageBuff = (int)(baseDamage * damageBuffList[i].strenght);
                buffDamage += porcentageBuff;
            }
            //OTHERWISE WE SIMPLY ADD.
            buffDamage += (int)damageBuffList[i].strenght;
        }

        newDamage = newDamage + buffDamage;
        return newDamage;
        
    }

    public static int GetResistance(int damage, DamageType damageType, CombatSlot attacked)
    {
        //WE APPLY THE DAMAGE AGAINST THE TARGET RESISTANT.
        //
        int newDamage = 0;

        int newResistance = 0;
        List<ConditionBase> resistanceBuff = GetBuffResistanceConditions(attacked.conditionHandler.conditionList);

        if (damageType == DamageType.Physical) 
        {
            newResistance = attacked.phyisicalResistance;
            int buffResistance = 0;

            for (int i = 0; i < resistanceBuff.Count; i++)
            {
                if(resistanceBuff[i]._condition.damageType == DamageType.Physical)
                {
                    if (resistanceBuff[i].isPorcentage)
                    {
                        int porcentageBuff = 0;

                        porcentageBuff += (int)(newResistance * resistanceBuff[i].strenght);
                        buffResistance += porcentageBuff;

                    }
                    buffResistance += (int)resistanceBuff[i].strenght;

                }

            }




        }


        if (damageType == DamageType.Magical)
        {
            newResistance = attacked.magicalResistance;
            int buffResistance = 0;

            for (int i = 0; i < resistanceBuff.Count; i++)
            {
                if (resistanceBuff[i]._condition.damageType == DamageType.Magical)
                {
                    if (resistanceBuff[i].isPorcentage)
                    {
                        int porcentageBuff = 0;

                        porcentageBuff += (int)(newResistance * resistanceBuff[i].strenght);
                        buffResistance += porcentageBuff;

                    }
                    buffResistance += (int)resistanceBuff[i].strenght;

                }

            }




        }



        //AND NOW WE CALCULATE DAMAGE AGAINST RESISTANCE.
        //HOW SHOULD WE DO THIS?
        //SHOULD IT BE LINEAR?
        //WHAT HAPPENS ABOUT NEGATIVE DAMAGE? 
        //IT IS LINEAR BUT THERE IS A MINIMUM DAMAGE RECEIVED. AND THATS 10% OF THE TOTAL DAMAGE.

        int mitigation = damage - newResistance;
        int minDamage = (int)(damage * 0.10);

        newDamage = mitigation;
        newDamage = Mathf.Clamp(newDamage, minDamage, 100000);


        return newDamage;
    }



    public static List<ConditionBase> GetBuffDamageConditions(List<ConditionBase> conditionList)
    {

        List<ConditionBase> newList = new List<ConditionBase>();

        for (int i = 0; i < conditionList.Count; i++)
        {
            if(conditionList[i]._condition.conditionType == ConditionType.Damage)
            {
                newList.Add(conditionList[i]);
            }         
        }

        return newList;
    }

    public static List<ConditionBase> GetBuffResistanceConditions(List<ConditionBase> conditionList)
    {
        List<ConditionBase> newList = new List<ConditionBase>();
        for (int i = 0; i < conditionList.Count; i++)
        {       
            if (conditionList[i]._condition.conditionType == ConditionType.Resistance)
            {
                newList.Add(conditionList[i]);
            }
        }
        return newList;
    }

    #endregion

    #region GETTING LIST FROM COMBAT
    public static List<AllyCombatSlot> CreateTargettableAllyList(SkillBase newSkill, List<AllyCombatSlot> slotList)
    {
        List<AllyCombatSlot> newList = new List<AllyCombatSlot>();

        if (newSkill.range == BattlePosition.Back)
        {
            //THEN IT ALWAYS HIT
            newList = slotList;
            return newList;
        }

        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].currentBattlePosition == BattlePosition.Front) newList.Add(slotList[i]);
        }


        for (int i = 0; i < newList.Count; i++)
        {
            if (newList[i].conditionHandler.HasCondition(ConditionType.Hidden))
            {
                newList.RemoveAt(i);
            }
        }

        return newList;
    }

    public static EnemyCombatSlot GetLowestHealthEnemy(List<EnemyCombatSlot> enemyList)
    {
        EnemyCombatSlot target = null;

        for (int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i].currentHealth == enemyList[i].currentHealth / 3)
            {
                if(target != null)
                {
                    if(enemyList[i].currentHealth < target.currentHealth)
                    {
                        target = enemyList[i];
                        continue;
                    }


                }

                target = enemyList[i];

            }

        }
        return target;

    }


    #endregion

    #region CONDITIONS
    public static bool IsBuff(ConditionBase _base)
    {
        if (_base._condition.conditionType == ConditionType.Protection || _base._condition.conditionType == ConditionType.Overwatch || _base._condition.conditionType == ConditionType.Regrowth
            || _base._condition.conditionType == ConditionType.Bless) return true;


        if (_base._condition.conditionType == ConditionType.Damage && _base.strenght > 0) return true;
        if (_base._condition.conditionType == ConditionType.Resistance && _base.strenght > 0) return true;


        return false;
    }

    #endregion


    public static ActionClass GetActionClass(CombatSlot actor, ActionType actionType, SkillBase _skill)
    {
        ActionClass newClass = new ActionClass();

        newClass.actor = actor;
        newClass.actionType = actionType;
        newClass.skill = _skill;

        return newClass;
    }

}



#region PLAYER
public enum PlayerStats
{
    Speech, 
    Barter, 
    Logistic,
    Medicine,
    Wisdom,
    Luck
}

#endregion

//WHAT HAPPENS WHEN AN ALLY GETS A LEVEL.
    //IT DOESNT IMPROVE STATS, BUT IT GIVES THE CHANCE TO DO SO.
    //THE CHARACTERS DONT HAVE DAMAGE BY THEMSELVS. THE DAMAGE IS LOCATED IN THE SKILL. THE SKILL DAMAGE SCALES WITH LEVEL, BUFFS AND EQUIPS.


    //THEY HAVE PERSONALITY
        //THERE WILL BE DIFFERENT PERSONALITIES THAT WILL AFFECT TO AN INVISIBLE PERSONALITY STAT LIST.



    //THEY HAVE HEALTH. 
    //THEY HAVE TENACITY. RESISTANCE AGAINST CC. IT DEFINES MORAL DAMAGE AS WELL.
    //THEY HAVE RESIST. RESISTANCE AGAINST ACTIVE CONDITIONS. ONLY THE APPLY NOT THE ACTUAL DAMAGE. 
    //THEY HAVE AGILITY. DEFINES SCOUTING OF ROOM. IT IS LOWERED WITH HEAVY ARMOR. HIGH ENOUGH AGILITY LETS YOU DODGE ATTACKS.
    //



    //WHATS THE DIFFERENCE BETWEEN BLEED AND POISON?
        //MAYBE POISON LOWERS OTHER THINGS: LIKE AGILITY? OR TENACITY? OR RESIST?


    //STATS THAT DETERMINE COMBAT
        //DAMAGE -
        //AGILITY - WHO GOES FIRST. DODGES ATTACK.
        //RESISTANCES AGAINST DAMAGE. 
        //RESISTANCE AGAINST CONDITIONS.



    //SACRED STATS - THESE ARE STATS THAT ARE UNIQUE. EACH GRANTS UNIQUE SKILLS. CAN ONLY BE INCREASED BY SPECIAL OBJECTS OR EVENTS.
        //DEMON, LIGHT, UNIVERSAL, DRAGON, 


//THERE ARE STATS THAT ARE IMPROVED AND STATS THAT ARE NOT.




//NARRATIONS ARE DIVIDED IN WHAT?