using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterBase : ScriptableObject
{
    //THIS IS THE ACTUAL CREATION OF THE UNIT THAT IS MEANT TO BE USED BY AN CLASS AS REFERENCE. THE CLASS WILL THEN USE IT TO HOLD INDIVUAL INFORMATION
    //HOWEVER YOU CANNOT PUT DICTIONARIES IN TEH EDITOR. SO YOU NEED
    //IT WILL BE CREATED ON ENABLED.
    //ALLIES HAVE GAIN STATS PER LEVEL AS WELL. ENEMIES DO NOT.
    //CHARACTERS HAVE MOVES. YOU CAN AT ONE TIME AT LEAST 4 OR 3 MOVES. BUT EACH CHARACTER HAS MOVES THAT THEY ACQUIRED AS THEY ADVANCE LEVELS.
    //BASE STATS ARE FORM 0 - 100
    [Header("BASE")]
    public Sprite charSprite;
    public string charName;

    [Header("HEALTH")]
    public int initialHealth;

    [Header("RESOURCE")]
    public ResourceType resourceType;
    public int initialMaxResource;

    [Header("MISC")]
    public BattlePosition battlePosition;

    [Header("STATS")]
    public List<CharacterStatsList> statList;
    public PassiveBase passive;
    public List<SkillBase> initialSkillList;
    public List<SkillBase> totalSkillList;

    //NEED TO CREATE THE LIST.
    //FOR NOW I WILL ONLY CARE ABOUT ABILITY.
    //



    private void OnEnable()
    {
        //GET THE LISTS
        if(statList == null)
        {
            statList = Utils.GetCharacterBaseList();
        }
        

    }

   public virtual AllyBase GetAlly()
    {
        return null;
    }
   public virtual EnemyBase GetEnemy()
    {
        return null;
    }

}

[System.Serializable]
public class CharacterStatsList
{
    public ChampionStat stat;
    public int value;
}


 public enum ChampionResourceType
{
    Mana, Stamina, Energy, Null
}
public enum ChampionStat
{
    Health,
    Resource,
    PhysicalDamage,
    MagicDamage,
    Tenacity,
    Agility,
    CritChance,
    CritDamage  
}
public enum ChampionScaling
{
    HealthScaling,
    ResourceScaling,
    PhysicalDamageScaling,
    MagicDamageScaling,
    TenacityScaling,
    AgilityScaling,
    CritChanceScaling,
    CritDamageScaling
}
public enum ChampionOtherStats
{
    Hunger, 
    Moral
}

public enum ResourceType
{
    Stamina,
    Mana,
    Energy
}
public enum BattlePosition
{
    Front, 
    Middle,
    Back
}
//
