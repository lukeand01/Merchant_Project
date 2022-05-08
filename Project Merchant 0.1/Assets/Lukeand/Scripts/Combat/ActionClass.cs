using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionClass
{
    //THIS IS A CLASS THAT WILL REPRESENT ACTIONS AND CARRY THEIR DATA TO WHOEVER MIGHT NEED.'
    //THINGS WE WANT TO NOTICE.

    //I ALSO NEED AN ID TO TELL WHO DID THE ACTION.
    //USING ACTION, HEALING, ATTACKING, SINGLE OR MULTIPLE, DAMAGE AND TYPE OF DAMAGE


    public CombatSlot actor;
    public ActionType actionType;
    public SkillBase skill; //ABILITY USED.

    //I WOULD TO BE ABLE TO CALL JUST SOME THINGS
    public void SetUp(CombatSlot _actor, ActionType _actionType, SkillBase _skill)
    {
        actor = _actor;

        actionType = _actionType;

        skill = _skill;
    }


}

public enum ActionType
{


}