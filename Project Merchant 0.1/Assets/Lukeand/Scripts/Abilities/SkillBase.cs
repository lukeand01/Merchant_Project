using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillBase : AbilityBase
{
    [Header("ABOUT SKILL")]
    public string skillName;
    public Sprite skillSprite;
    [TextArea] public string skillDescription;

    [Header("TYPES")]
    public ResourceType resourceType;
    public TargetType targetType;
    public DamageType damageType;
    public List<ConditionBase> conditionList = new List<ConditionBase>();
   
    [Header("STATS")]
    public int resourceCost;
    public int strenght;
    public BattlePosition range;
    public float levelScaling; //HOW MUCH DAMAGE DOES IT GAIN FROM LEVEL.

    public virtual SingleTargetSkill GetSS()
    {
        return null;
    }
    public virtual MultipleTargetSkill GetMS()
    {
        return null;
    }

}

