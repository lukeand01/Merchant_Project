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