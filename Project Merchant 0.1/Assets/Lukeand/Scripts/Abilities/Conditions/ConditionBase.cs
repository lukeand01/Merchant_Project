using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConditionBase 
{

    public Condition _condition;
    
    [Tooltip("AGAINST TENACITY. CHANCE OF HITTING.")]
    public int chance; 
    [Tooltip("THE STATS OF WHAT IT DOES. APPLY FOR BLEED AND ALIKE")]
    public float strenght; //
    [Tooltip("INITIAL TURNS ")]
    public int persistence;
    public bool isPorcentage;

    [HideInInspector] public int currentStacks;
    [HideInInspector] public CombatSlot actor; //HOWEVER CAUSED THE CONDITION.
    [HideInInspector] public int id; //THIS IS SO WE CAN IDENTIFTY.

    public void SetUp(int _id, CombatSlot target, CombatSlot attacker)
    {


        actor = attacker;
        id = _id;

        _condition.SetUp(this, target, attacker);
    }



    public void Act(CombatSlot target, CombatSlot attacker)
    {
        _condition.Act(target, attacker, (int)strenght); //THEN WE PUT ALL THE VARIABLES THERE.
    }


    public void Close(CombatSlot target, CombatSlot attacker)
    {
        _condition.Close(this, target, attacker);
    }

    //
    
}

public enum ConditionType
{
    Stun, 
    Bleed, 
    Poison,
    Blind, //HAS MISSING CHANCE.
    Silence, // CANT USE LAST USED ABILITY
    Confused, //WILL ATTACK ALLY.
    Manipulated, //WILL ATTACK ALLY OR HELP ENEMY.
    Cursed, //TAKES MORE DAMAGE FROM AN ENEMY.
    Fear, //CANNOT ATTACK 
    Protection, //NEXT ATTACK IS COMPLETELY NEUTRALIZED.
    Damage, //CAN LOSE OR GAIN DAMAGE STATS
    Resistance, //GAIN ESPECIFIC 
    Overwatch, //THIS UNIT IS BEING PROTECTED BY ANOTHER UNIT.
    Regrowth, //HEALS EVERY TURN
    Bless,
    Hidden, //CANNOT BE TARGETTED
    Taunt //ENEMIES WANT TO ATTACK THIS TARGET MORE. THERE IS A ROLL BASED IN STRENGHT
}





//HIDDEN NEEDS:
    //TO ACTIVE IN THE MOMENT YOU RECEIVE IT.
    //TO SIGNAL TO OTHERS THAT IT IS HIDDEN.
    //TO BE REMOVED ONCE YOU DO ANY ACTION.
    //IT GAVES A DAMAGE BUFF. WHERE DO WE HOLD BUFFS?

//OVERWATCH NEEDS:
    //TO GIVE A REFERENCE OF THE PROTECTOR TO THE TARGET.
    //IF THE PROTECTOR MARKS SOMEONE ELSE THE MARK IS REMOVE FROM THE ORIGINAL TARGET.
    //IF THE TARGET IS ATTACKED THE PROTECTOR WILL TAKE THE DAMAGE.

//BLIND
    //IT ONLY PROCS WHEN YOU ATTACK.
    //WHEN YOU ATTACK AND HAVE THE CONDITION THERE IS A CHANCE TO MISS.



