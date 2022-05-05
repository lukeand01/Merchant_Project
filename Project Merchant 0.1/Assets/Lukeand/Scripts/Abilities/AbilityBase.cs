using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : ScriptableObject
{
    //


}

public enum TargetType
{
    Self, //THIS MEANS ONCE YOU USE IT GOES DIRECTLY TO YOU WITHOUT TARGET
    Enemy,
    Ally,
    Any,
    Null
}
public enum DamageType
{
    Physical,
    Magical,
    True,
    Heal
}